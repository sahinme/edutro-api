using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EgitimAPI.ApplicationCore.Services.UserService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Entities;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Educators;
using Microsoft.EgitimAPI.ApplicationCore.Entities.TenantEducator;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Users;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;
using Microsoft.EgitimAPI.ApplicationCore.Services.BlobService;
using Microsoft.EgitimAPI.ApplicationCore.Services.Category.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.CourseService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.EducatorService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.PasswordHasher;
using Microsoft.EgitimAPI.ApplicationCore.Services.TenantService.Dto;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.EducatorService
{
    public class EducatorAppService:IEducatorAppService
    {
        private readonly IAsyncRepository<Educator> _educatorRepository;
        private readonly IAsyncRepository<TenantEducator> _tenantEducatorRepository;
        private readonly IBlobService _blobService;

        public EducatorAppService(IAsyncRepository<Educator> educatorRepository,
            IAsyncRepository<TenantEducator> tenantEducatorRepository,
            IBlobService blobService
        )
        {
            _educatorRepository = educatorRepository;
            _tenantEducatorRepository = tenantEducatorRepository;
            _blobService = blobService;
        }
        
        public async Task<Educator> CreateEducator(CreateEducatorDto input)
        {
            var logoPath = await _blobService.InsertFile(input.File);
            var hashedPassword = SecurePasswordHasherHelper.Hash(input.Password);
            var educator = new Educator
            {
                Name = input.Name,
                Surname = input.Surname,
                Email = input.Email,
                Password = hashedPassword,
                Profession = input.Profession,
                Resume = input.Resume,
                ProfileImagePath = logoPath
            };
            await _educatorRepository.AddAsync(educator);
            
            return educator;
        }

        public async Task<List<EducatorDto>> GetAllEducators()
        {
            var educators = await _educatorRepository.GetAll().Where(x=>x.IsDeleted==false)
                .Include(x=>x.EducatorTenants)
                .ThenInclude(x=>x.Tenant)
                .Include(x=>x.GivenCourses).ThenInclude(x=>x.Course)
                .Select(x => new EducatorDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    Profession = x.Profession,
                    Resume = x.Resume,
                    ProfileImagePath = BlobService.BlobService.GetImageUrl(x.ProfileImagePath),
                    Score = x.Score,
                    EducatorTenants = x.EducatorTenants.Where(tenant=>tenant.IsAccepted).Select(tenant => new EducatorTenantDto
                    {
                        TenantId = tenant.Tenant.Id,
                        TenantName = tenant.Tenant.TenantName
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
                }).ToListAsync();
            return educators;
        }

        public async Task<List<EducatorDto>> GetEducatorByName(string educatorName)
        {
            var educator = await _educatorRepository.GetAll()
                .Where(x => x.Name.Contains(educatorName) || x.Surname.Contains(educatorName))
                .Include(x => x.EducatorTenants)
                .ThenInclude(x => x.Tenant)
                .Include(x => x.GivenCourses).ThenInclude(x => x.Course)
                .Select(x => new EducatorDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    Profession = x.Profession,
                    Resume = x.Resume,
                    ProfileImagePath = BlobService.BlobService.GetImageUrl(x.ProfileImagePath),
                    Score = x.Score,
                    EducatorTenants = x.EducatorTenants.Where(tenant=>tenant.IsAccepted).Select(tenant => new EducatorTenantDto
                    {
                        TenantId = tenant.Tenant.Id,
                        TenantName = tenant.Tenant.TenantName
                    }).ToList(),
                    Courses = x.GivenCourses.Select(course => new CourseDto
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
                    }).ToList()
                }).ToListAsync();
            return educator;
        }

        public async Task<EducatorDto> GetEducatorById(long educatorId)
        {
            var educator = await _educatorRepository.GetAll()
                .Include(x => x.EducatorTenants)
                .ThenInclude(x => x.Tenant)
                .Include(x => x.GivenCourses).ThenInclude(x => x.Course)
                .Select(x => new EducatorDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    Profession = x.Profession,
                    Resume = x.Resume,
                    ProfileImagePath = BlobService.BlobService.GetImageUrl(x.ProfileImagePath),
                    Score = x.Score,
                    EducatorTenants = x.EducatorTenants.Where(tenant=>tenant.IsAccepted).Select(tenant => new EducatorTenantDto
                    {
                        TenantId = tenant.Tenant.Id,
                        TenantName = tenant.Tenant.TenantName
                    }).ToList(),
                    Courses = x.GivenCourses.Select(course => new CourseDto
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
                    }).ToList()
                }).FirstOrDefaultAsync(x=>x.Id==educatorId);
            return educator;
        }

        public async Task HandleSubscribeTenant(SubscribeDto input)
        {
            var relation = await _tenantEducatorRepository.GetAll()
                .FirstOrDefaultAsync(x => x.EducatorId == input.EducatorId && x.TenantId == input.TenantId);
            relation.IsAccepted = input.IsAccepted;
            await _tenantEducatorRepository.UpdateAsync(relation);
        }

        public async Task Delete(long id)
        {
            var educator = await _educatorRepository.GetByIdAsync(id);
            educator.IsDeleted = true;
            await _educatorRepository.UpdateAsync(educator);

            var tenantEducators = await _tenantEducatorRepository.GetAll().Where(x => x.EducatorId == id).ToListAsync();
            foreach (var tenantEducator in tenantEducators)
            {
                tenantEducator.IsDeleted = true;
                await _tenantEducatorRepository.UpdateAsync(tenantEducator);
            }
        }

        public async Task<Educator> UpdateEducator(UpdateEducatorDto input)
        {
            var educator = await _educatorRepository.GetByIdAsync(input.Id);
            educator.Name = input.Name;
            educator.Surname = input.Surname;
            educator.Profession = input.Profession;
            educator.Resume = input.Resume;

            await _educatorRepository.UpdateAsync(educator);
            return educator;
        }

        public async Task<List<EducatorSearchDto>> GetEducatorByEmail(string email)
        {
            var educator = await _educatorRepository.GetAll()
                .Where(x => x.IsDeleted == false && x.Email == email || x.Email.Contains(email)).Select(x =>
                    new EducatorSearchDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Surname = x.Surname,
                        Profession = x.Profession,
                        LogoPath = BlobService.BlobService.GetImageUrl(x.ProfileImagePath)
                    }).ToListAsync();
            return educator;
        }
        
        public async Task<EducatorLoginDto> Login(TenantOrEducatorLoginDto input)
        {
            var user = await _educatorRepository.GetAll()
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
            
            var result = new EducatorLoginDto
            {
                Id = user.Id, EntityType = EntityType.Educator, Name = user.Name,Surname = user.Surname
            };

            return result;
        }
        
    }
}