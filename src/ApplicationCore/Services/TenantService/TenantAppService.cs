using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Courses;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Notifications;
using Microsoft.EgitimAPI.ApplicationCore.Entities.TenantEducator;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;
using Microsoft.EgitimAPI.ApplicationCore.Services.Category.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.CommentService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.CourseService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.NotificationService;
using Microsoft.EgitimAPI.ApplicationCore.Services.NotificationService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.TenantService.Dto;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.TenantService
{
    public class TenantAppService:ITenantAppService
    {
        private readonly IAsyncRepository<Tenant> _tenantRepository;
        private readonly IAsyncRepository<TenantEducator> _tenantEducatorRepository;
        private readonly INotificationAppService _notificationAppService;

        public TenantAppService(IAsyncRepository<Tenant> tenantRepository,IMapper mapper,
            IAsyncRepository<TenantEducator> tenantEducatorRepository,
            INotificationAppService notificationAppService
            )
        {
            _tenantRepository = tenantRepository;
            _tenantEducatorRepository = tenantEducatorRepository;
            _notificationAppService = notificationAppService;
        }
        
        public async Task CreateTenant(CreateTenantDto input)
        {
            //var user = _mapper.Map<Tenant>(input);  
            var user = new Tenant
            {
                TenantName = input.TenantName,
                Address = input.Address,
                IsPremium = input.IsPremium,
                PhoneNumber = input.PhoneNumber,
                PhoneNumber2 = input.PhoneNumber2,
                Password = input.Password
            };
            await _tenantRepository.AddAsync(user);
        }

        public async Task<TenantDto> GetTenantById(long id)
        {
            var result = await _tenantRepository.GetAll()
                .Include(x=>x.TenantEducators).ThenInclude(x=>x.Educator)
                .Include(x=>x.GivenCourses).ThenInclude(x=>x.Course).ThenInclude(x=>x.Category)
                .Include(x=>x.Comments).Select(x => new TenantDto
                {
                    Id = x.Id,
                    TenantName = x.TenantName,
                    Score = x.Score,
                    PhoneNumber = x.PhoneNumber,
                    PhoneNumber2 = x.PhoneNumber2,
                    LogoPath = x.LogoPath,
                    IsPremium = x.IsPremium,
                    Address = x.Address,
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
                throw new Exception("this tenant dont exist!");
            }
            return result;
        }

        
        public  List<TenantDto> GetAll()
        {
            var model =  _tenantRepository.GetAll()
                .Include(x=>x.TenantEducators).ThenInclude(x=>x.Educator)
                .Include(x=>x.GivenCourses).ThenInclude(x=>x.Course).ThenInclude(x=>x.Category)
                .Include(x=>x.Comments)
                .Where(x => x.IsDeleted == false).Select(x => new TenantDto
            {
                Id = x.Id,
                TenantName = x.TenantName,
                Score = x.Score,
                PhoneNumber = x.PhoneNumber,
                PhoneNumber2 = x.PhoneNumber2,
                LogoPath = x.LogoPath,
                IsPremium = x.IsPremium,
                Address = x.Address,
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
                OwnerType = "Educator",
                SenderId = input.TenantId,
                SenderType = "Tenant",
                Content = tenant.TenantName+" "+"sizi eğitmen olarak eklemek istiyor.",
                NotifyContentType = NotifyContentType.SubscribeRequest
            });
        }
    }
}