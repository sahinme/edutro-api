using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Courses;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Educators;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;
using Microsoft.EgitimAPI.ApplicationCore.Services.Category.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.CourseService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.GivenCourseService;
using Microsoft.EgitimAPI.ApplicationCore.Services.GivenCourseService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.TenantService.Dto;
using Microsoft.EgitimAPI.Lib;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.CourseService
{
    public class CourseAppService:ICourseAppService
    {
        private readonly IAsyncRepository<Course> _courseRepository;
        private readonly IAsyncRepository<GivenCourse> _givenCourseRepository;
        private readonly ICheckEdition _checkEdition;

        public CourseAppService(IAsyncRepository<Course> courseRepository,
            IAsyncRepository<GivenCourse> givenCourseRepository,
                ICheckEdition checkEdition
            )
        {
            _courseRepository = courseRepository;
            _givenCourseRepository =givenCourseRepository;
            _checkEdition = checkEdition;
        }
        public async Task<Course> CreateCourse(CreateCourseDto input)
        {
            if (input.EducatorId.Length>0)
            {
                var isHaveRightEducator = await _checkEdition.HaveCreateCourseRight<Educator>(input.EducatorId);
                if (!isHaveRightEducator)
                {
                    throw new Exception("No right to create course for educator !");
                }
            }

            if ( input.TenantId.Length>0)
            {
                var isHaveRight = await _checkEdition.HaveCreateCourseRight<Tenant>(input.TenantId);
                if (!isHaveRight)
                {
                    throw new Exception("No right to create course tenant !");
                }
            }
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

            var count = (input.TenantId.Length > input.EducatorId.Length)
                ? input.TenantId.Length
                : input.EducatorId.Length;
            
            for (var i = 0; i <count  ; i++)
            {
                var givenCourse = new GivenCourse
                {
                    CourseId = course.Id,
                    EducatorId = input.EducatorId[i],
                    TenantId = input.TenantId[i]
                };
                await _givenCourseRepository.AddAsync(givenCourse);
            }

            return course;
        }
        public async Task<List<CourseDto>> GetCoursesByName(string courseName)
        {
            var course = await _courseRepository.GetAll().Include(x=>x.Category)
                .Include(x => x.Owners).ThenInclude(x => x.Tenant)
                .Include(x=>x.Owners).ThenInclude(x=>x.Educator)
                .Where(x => x.Title.Contains(courseName) || x.Description.Contains(courseName))
                .Select(x => new CourseDto
            {
                Id = x.Id,
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
                },
                Tenants = x.Owners.Select(t => new CourseTenantDto
                {
                    TenantId = t.Tenant.Id,
                    TenantName = t.Tenant.TenantName,
                    LogoPath = t.Tenant.LogoPath
                }).ToList(),
                Educators = x.Owners.Select(e=>new CourseEducatorDto
                {
                    EducatorId = e.Educator.Id,
                    EducatorName=e.Educator.Name,
                    Profession = e.Educator.Profession,
                    ProfileImgPath = e.Educator.ProfileImagePath
                }).ToList()
            }).ToListAsync();
            return course;
        }

        public async Task<List<CourseDto>> GetCoursesByCategory(long categoryId)
        {
            var courses = await _courseRepository.GetAll().Include(x => x.Category)
                .Include(x => x.Owners).ThenInclude(x => x.Tenant)
                .Include(x=>x.Owners).ThenInclude(x=>x.Educator)
                .Where(x => x.Category.Id == categoryId).Select(x => new CourseDto
                {
                    Id = x.Id,
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
                    },
                    Tenants = x.Owners.Select(t => new CourseTenantDto
                    {
                        TenantId = t.Tenant.Id,
                        TenantName = t.Tenant.TenantName,
                        LogoPath = t.Tenant.LogoPath
                    }).ToList(),
                    Educators = x.Owners.Select(e=>new CourseEducatorDto
                    {
                        EducatorId = e.Educator.Id,
                        EducatorName=e.Educator.Name,
                        Profession = e.Educator.Profession,
                        ProfileImgPath = e.Educator.ProfileImagePath
                    }).ToList()
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
            course.CategoryId = input.CategoryId;

            await _courseRepository.UpdateAsync(course);

        }

        public async Task<List<CourseDto>> GetAllCourses()
        {
            var courses = await _courseRepository.GetAll().Where(x => x.IsDeleted == false)
                .Include(x => x.Owners).ThenInclude(x => x.Tenant)
                .Include(x=>x.Owners).ThenInclude(x=>x.Educator)
                .Include(x => x.Category)
                .Select(x => new CourseDto
                {
                    Id = x.Id,
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
                    },
                    Tenants = x.Owners.Select(t => new CourseTenantDto
                    {
                        TenantId = t.Tenant.Id,
                        TenantName = t.Tenant.TenantName,
                        LogoPath = t.Tenant.LogoPath
                    }).ToList(),
                    Educators = x.Owners.Select(e=>new CourseEducatorDto
                    {
                        EducatorId = e.Educator.Id,
                        EducatorName=e.Educator.Name,
                        Profession = e.Educator.Profession,
                        ProfileImgPath = e.Educator.ProfileImagePath
                    }).ToList()
                }).ToListAsync();
            return courses;
        }
    }
}