using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using AutoMapper;
using EgitimAPI.ApplicationCore.Services.UserService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Entities;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Courses;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Notifications;
using Microsoft.EgitimAPI.ApplicationCore.Entities.TenantEducator;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;
using Microsoft.EgitimAPI.ApplicationCore.Services.BlobService;
using Microsoft.EgitimAPI.ApplicationCore.Services.Category.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.CommentService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.CourseService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.NotificationService;
using Microsoft.EgitimAPI.ApplicationCore.Services.NotificationService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.PasswordHasher;
using Microsoft.EgitimAPI.ApplicationCore.Services.TenantService.Dto;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.TenantService
{
    public class TenantAppService:ITenantAppService
    {
        private readonly IAsyncRepository<Tenant> _tenantRepository;
        private readonly IAsyncRepository<TenantEducator> _tenantEducatorRepository;
        private readonly INotificationAppService _notificationAppService;
        private readonly IBlobService _blobService;
        public TenantAppService(IAsyncRepository<Tenant> tenantRepository,IMapper mapper,
            IAsyncRepository<TenantEducator> tenantEducatorRepository,
            INotificationAppService notificationAppService,
            IBlobService blobService
            )
        {
            _tenantRepository = tenantRepository;
            _tenantEducatorRepository = tenantEducatorRepository;
            _notificationAppService = notificationAppService;
            _blobService = blobService;
        }
        
        public async Task CreateTenant(CreateTenantDto input)
        {
           
            var hashedPassword = SecurePasswordHasherHelper.Hash(input.Password);
            var user = new Tenant
            {
                TenantName = input.TenantName,
                Address = input.Address,
                IsPremium = input.IsPremium,
                PhoneNumber = input.PhoneNumber,
                PhoneNumber2 = input.PhoneNumber2,
                Password = hashedPassword,
                Email = input.Email,
                AboutUs = input.AboutUs,
                Title = input.Title,
                LocationId = input.LocationId,
            };
            if (input.LogoFile!=null)
            {
                var logoPath = await _blobService.InsertFile(input.LogoFile);
                user.LogoPath = logoPath;
            }
            await _tenantRepository.AddAsync(user);
        }

        public async Task<Tenant> UpdateTenant(UpdateTenantDto input)
        {
            var tenant = await _tenantRepository.GetByIdAsync(input.Id);
            if (tenant == null)
            {
                throw new Exception("Tenant bulunamadi");
            }
            tenant.Address = input.Address;
            tenant.Email = input.Email;
            tenant.LocationId = input.LocationId;
            tenant.Title = input.Title;
            tenant.TenantName = input.TenantName;
            tenant.AboutUs = input.AboutUs;
            tenant.PhoneNumber = input.PhoneNumber;
            tenant.PhoneNumber2 = input.PhoneNumber2;

            if (input.LogoFile == null) return tenant;
            var filePath = await _blobService.InsertFile(input.LogoFile);
            tenant.LogoPath = filePath;

            return tenant;
        }

        public async Task<TenantDto> GetTenantById(long id)
        {
            var result = await _tenantRepository.GetAll()
                .Include(x=>x.TenantEducators).ThenInclude(x=>x.Educator)
                .Include(x=>x.GivenCourses).ThenInclude(x=>x.Course).ThenInclude(x=>x.Category)
                .Include(x=>x.Location)
                .Include(x=>x.Comments).Select(x => new TenantDto
                {
                    Id = x.Id,
                    TenantName = x.TenantName,
                    Score = x.Score,
                    PhoneNumber = x.PhoneNumber,
                    LogoPath = BlobService.BlobService.GetImageUrl(x.LogoPath),
                    PhoneNumber2 = x.PhoneNumber2,
                    Email = x.Email,
                    IsPremium = x.IsPremium,
                    Title = x.Title,
                    AboutUs = x.AboutUs,
                    Address = x.Address,
                    LocationId = x.Location.Id,
                    LocationName = x.Location.Name,
                    TenantEducators = x.TenantEducators.Where(educator=>educator.IsAccepted).Select(educator => new TenantEducatorDto
                    {
                        EducatorId = educator.Educator.Id,
                        EducatorName = educator.Educator.Name+" "+educator.Educator.Surname,
                        Profession = educator.Educator.Profession
                    }).ToList(),
                    Courses = x.GivenCourses.Select(course=>new CourseDto
                    {
                        Id = course.Course.Id,
                        Title = course.Course.Title,
                        Description = course.Course.Description,
                        StartDate = course.Course.StartDate,
                        EndDate = course.Course.EndDate,
                        Quota = course.Course.Quota,
                        Price = course.Course.Price,
                        Category = new CategoryDto
                        {
                            DisplayName = course.Course.Category.DisplayName,
                            Id = course.Course.Category.Id,
                            Description = course.Course.Category.Description,
                        }
                    }).ToList(),
                }).FirstOrDefaultAsync(x=>x.Id==id);
            if (result==null)
            {
                throw new Exception("Bu kullanıcı bulunamadı");
            }
            return result;
        }
        
          public async Task<List<TenantEducatorListDto>> GetEducators(long id)
          {
              var result = await _tenantRepository.GetAll()
                  .Include(x => x.TenantEducators).ThenInclude(x => x.Educator)
                  .Where(x => x.Id == id).Select(x => new TenantEducatorListDto
                  {
                      TenantEducators = x.TenantEducators.Where(e => e.IsDeleted == false).Select(e =>
                          new TenantEducatorsDto
                          {
                              Id = e.Educator.Id,
                              IsAccepted = e.IsAccepted,
                              Name = e.Educator.Name,
                              Surname = e.Educator.Surname,
                              Profession = e.Educator.Profession,
                              ProfileImagePath = BlobService.BlobService.GetImageUrl(e.Educator.ProfileImagePath)
                          }).ToList()
                  }).ToListAsync();
            return result;
        }

        
        public  List<TenantDto> GetAll()
        {
            var model =  _tenantRepository.GetAll()
                .Include(x=>x.Location)
                .Where(x => x.IsDeleted == false).Select(x => new TenantDto
            {
                Id = x.Id,
                TenantName = x.TenantName,
                Score = x.Score,
                LocationName = x.Location.Name,
                Title = x.Title,
                LogoPath = BlobService.BlobService.GetImageUrl(x.LogoPath),
                Address = x.Address,
            }).ToList();

            return model;
        }

        public async Task Delete(long id)
        {
            var tenant = await _tenantRepository.GetByIdAsync(id);
            tenant.IsDeleted = true;
            await _tenantRepository.UpdateAsync(tenant);
            
            var educatorTenants = await _tenantEducatorRepository.GetAll().Where(x => x.TenantId == id).ToListAsync();
            foreach (var educatorTenant in educatorTenants)
            {
                educatorTenant.IsDeleted = true;
                await _tenantEducatorRepository.UpdateAsync(educatorTenant);
            }
        }
        

        public async Task AddEducator(CreateTenantEducatorDto input)
        {
            var isExist = await _tenantEducatorRepository.GetAll()
                .Where(x => x.EducatorId == input.EducatorId && x.TenantId == input.TenantId).ToListAsync();
            
            if (isExist.Count != 0 )
            {
                throw new Exception("Bu egitmen zaten ekli!");
            }
            var tenant = await _tenantRepository.GetByIdAsync(input.TenantId);
            var model = new TenantEducator
            {
                EducatorId = input.EducatorId,
                TenantId = input.TenantId
            };
            await _tenantEducatorRepository.AddAsync(model);
            await _notificationAppService.CreateNotify(new CreateNotificationDto
            {
                OwnerId = input.EducatorId,
                OwnerType = EntityType.Educator,
                SenderId = input.TenantId,
                SenderType = EntityType.Tenant,
                Title = "Ekleme isteği",
                Content = tenant.TenantName+" "+"sizi eğitmen olarak eklemek istiyor.",
                NotifyContentType = NotifyContentType.SubscribeRequest
            });
        }
        
        public async Task<TenantLoginDto> Login(TenantOrEducatorLoginDto input)
        {
            var user = await _tenantRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Email == input.Email);
            if (user == null)
            {
                throw new Exception("There is no user!");
            }
            var decodedPassword = SecurePasswordHasherHelper.Verify(input.Password, user.Password);
            if (!decodedPassword)
            {
                return null;
            }

            var result = new TenantLoginDto
            {
                Id = user.Id, EntityType = EntityType.Tenant, TenantName = user.TenantName
            };

            return result;
        }
    }
}