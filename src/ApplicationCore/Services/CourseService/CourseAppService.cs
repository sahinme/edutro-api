using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Courses;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;
using Microsoft.EgitimAPI.ApplicationCore.Services.Category.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.CourseService.Dto;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.CourseService
{
    public class CourseAppService:ICourseAppService
    {
        private readonly IAsyncRepository<Course> _courseRepository;

        public CourseAppService(IAsyncRepository<Course> courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task CreateCourse(CreateCourseDto input)
        {
            var course = new Course
            {
                Title = input.Title,
                Description = input.Description,
                Quota = input.Quota,
                Price = input.Price,
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                CategoryId = input.CategoryId,
                TenantId = input.TenantId
            };
            await _courseRepository.AddAsync(course);
        }

        public async Task<List<CourseDto>> GetCoursesByName(string courseName)
        {
            var course = await _courseRepository.GetAll().Include(x=>x.Category)//.ThenInclude(x=>x.ParentCategory)
                .Where(x => x.Title == courseName).Select(x => new CourseDto
            {
                Title = x.Title,
                Description = x.Description,
                Quota = x.Quota,
                Price = x.Price,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Category = new CategoryDto
                {
                    Id = x.Category.Id,
                    Description = x.Category.Description,
                    DisplayName = x.Category.DisplayName,
                    ParentCategory = new ParentCategoryDto()
//                    {
//                        Id = x.Category.ParentCategory.Id,
//                        DisplayName = x.Category.ParentCategory.DisplayName,
//                        Description = x.Category.ParentCategory.Description
//                    }
                }
            }).ToListAsync();
            return course;
        }

        public async Task<List<CourseDto>> GetCoursesByCategory(long categoryId)
        {
            var courses = await _courseRepository.GetAll().Include(x => x.Category)//.ThenInclude(x=>x.ParentCategory)
                .Where(x => x.Category.Id == categoryId).Select(x => new CourseDto
                {
                    Title = x.Title,
                    Description = x.Description,
                    Quota = x.Quota,
                    Price = x.Price,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Category = new CategoryDto
                    {
                        Id = x.Category.Id,
                        Description = x.Category.Description,
                        DisplayName = x.Category.DisplayName,
                        ParentCategory = new ParentCategoryDto()
//                        {
//                            Id = x.Category.ParentCategory.Id,
//                            DisplayName = x.Category.ParentCategory.DisplayName,
//                            Description = x.Category.ParentCategory.Description
//                        }
                    }
                }).ToListAsync();
            return courses;
        }
    }
}