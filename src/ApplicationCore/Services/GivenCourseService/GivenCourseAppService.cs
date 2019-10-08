using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Courses;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;
using Microsoft.EgitimAPI.ApplicationCore.Services.Category.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.CourseService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.GivenCourseService.Dto;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.GivenCourseService
{
    public class GivenCourseAppService:IGivenCourseAppService
    {
        private readonly IAsyncRepository<GivenCourse> _givenCourseRepository;

        public GivenCourseAppService(IAsyncRepository<GivenCourse> givenCourseRepository)
        {
            _givenCourseRepository = givenCourseRepository;
        }
        
        // TODO: Educator dto eklenecek. Data cekilip nullCheck yapilacak ona gore tenant yada educator dto doldurulacak.
        public async Task<List<GivenCourseDto>> GetAllGivenCourses()
        {
            var givenCourses = await _givenCourseRepository.GetAll().Include(x => x.Course).ThenInclude(x=>x.Category)
                //.ThenInclude(x=>x.ParentCategory)
                .Include(x => x.Tenant)
                .Include(x=>x.Educator)
                .Where(x=>x.Course.IsDeleted==false)
                .Select(x => new GivenCourseDto
                {
                    Id   = x.Id,
                    Course = new CourseDto
                    {
                        Id = x.Course.Id,
                        Title = x.Course.Title,
                        Description = x.Course.Description,
                        Quota = x.Course.Quota,
                        Price = x.Course.Price,
                        StartDate = x.Course.StartDate,
                        EndDate = x.Course.EndDate,
                        Category = new CategoryDto
                        {
                            Id = x.Course.Category.Id,
                            Description = x.Course.Category.Description,
                            DisplayName = x.Course.Category.DisplayName,
                            ParentCategory = new ParentCategoryDto()
//                            {
//                                Id = x.Course.Category.ParentCategory.Id,
//                                Description = x.Course.Category.ParentCategory.Description,
//                                DisplayName = x.Course.Category.ParentCategory.DisplayName
//                            }
                        }
                    },
                    TenantName = x.Tenant.TenantName,
                    LogoPath = x.Tenant.LogoPath,
                    TenantId = x.Tenant.Id,
                    EducatorId = x.Educator.Id,
                    EducatorFullName = x.Educator.Name+" "+x.Educator.Surname
                }).ToListAsync();
            return givenCourses;
        }

        public async Task CreateGivenCourse(CreateGivenCourseDto input)
        {
            var givenCourse = new GivenCourse
            {
                CourseId = input.CourseId,
                TenantId = input.TenantId,
                EducatorId = input.EducatorId
            };
            await _givenCourseRepository.AddAsync(givenCourse);
        }
    }
}