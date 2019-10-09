using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Courses;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;
using Microsoft.EgitimAPI.ApplicationCore.Services.Category.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.CourseService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.GivenCourseService;
using Microsoft.EgitimAPI.ApplicationCore.Services.GivenCourseService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.TenantService.Dto;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.CourseService
{
    public class CourseAppService:ICourseAppService
    {
        private readonly IAsyncRepository<Course> _courseRepository;
        private readonly IGivenCourseAppService _givenCourseAppService;

        public CourseAppService(IAsyncRepository<Course> courseRepository,
                IGivenCourseAppService givenCourseAppService
            )
        {
            _courseRepository = courseRepository;
            _givenCourseAppService = givenCourseAppService;
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
            };
            await _courseRepository.AddAsync(course);
            var givenCourse = new CreateGivenCourseDto
            {
                CourseId = course.Id,
                TenantId = input.TenantId,
                EducatorId = input.EducatorId
            };
            await _givenCourseAppService.CreateGivenCourse(givenCourse);
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

        public async Task DeleteCourse(long id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            course.IsDeleted = true;
            await _courseRepository.UpdateAsync(course);
        }

        public async Task UpdateCourse(UpdateCourseDto input)
        {
            var isExistCourse = await _courseRepository.GetAll().AnyAsync(x => x.Id == input.Id);
                if (!isExistCourse)
                    throw new Exception("Dont exist this course!");

            var course = await _courseRepository.GetAll().FirstAsync(x => x.Id == input.Id);

            course.Id = input.Id;    
            course.Description = input.Description;
            course.Title = input.Title;
            course.Price = input.Price;
            course.Quota = input.Quota;
            course.StartDate = input.StartDate;
            course.EndDate = input.EndDate;

            await _courseRepository.UpdateAsync(course);

                ///// given course update edilecek.
//            if (input.EducatorId!=null)
//            {
//                var givenCourse = await _givenCourseAppService.ge
//            }
        }

        public async Task<List<CourseDto>> GetAllCourses()
        {
            var courses = await _courseRepository.GetAll().Where(x => x.IsDeleted == false)
                .Include(x => x.Tenants).ThenInclude(x => x.Tenant)
                .Include(x => x.Category)
                .Select(x => new CourseDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Quota = x.Quota,
                    Price = x.Price,
                    StartDate = x.StartDate.GetValueOrDefault(),
                    EndDate = x.EndDate.GetValueOrDefault(),
                    Category = new CategoryDto
                    {
                        Id = x.Category.Id,
                        Description = x.Category.Description,
                        DisplayName = x.Category.DisplayName,
                        ParentCategory = new ParentCategoryDto()
                    },
                    Tenants = x.Tenants.Select(t => new CourseTenantDto
                    {
                        TenantId = t.Tenant.Id,
                        TenantName = t.Tenant.TenantName,
                        LogoPath = t.Tenant.LogoPath
                    }).ToList()
                }).ToListAsync();
            return courses;
        }
    }
}