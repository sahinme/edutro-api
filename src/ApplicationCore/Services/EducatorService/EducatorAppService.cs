using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Educators;
using Microsoft.EgitimAPI.ApplicationCore.Entities.TenantEducator;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;
using Microsoft.EgitimAPI.ApplicationCore.Services.Category.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.CourseService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.EducatorService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.TenantService.Dto;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.EducatorService
{
    public class EducatorAppService:IEducatorAppService
    {
        private readonly IAsyncRepository<Educator> _educatorRepository;
        private readonly IAsyncRepository<TenantEducator> _tenantEducatorRepository;

        public EducatorAppService(IAsyncRepository<Educator> educatorRepository,
            IAsyncRepository<TenantEducator> tenantEducatorRepository
        )
        {
            _educatorRepository = educatorRepository;
            _tenantEducatorRepository = tenantEducatorRepository;
        }
        
        public async Task CreateEducator(CreateEducatorDto input)
        {
            var educator = new Educator
            {
                Name = input.Name,
                Surname = input.Surname,
                Profession = input.Profession,
                Resume = input.Resume,
            };
            await _educatorRepository.AddAsync(educator);
            
            var tenantEducator = new TenantEducator
            {
                EducatorId = educator.Id,
                TenantId = input.TenantId
            };
            await _tenantEducatorRepository.AddAsync(tenantEducator);
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
                    EducatorTenants = x.EducatorTenants.Select(tenant => new EducatorTenantDto
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
                    }).ToList()
                }).ToListAsync();
            return educators;
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
    }
}