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
using Microsoft.EgitimAPI.ApplicationCore.Services.Dto;
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
        private readonly IAsyncRepository<AdvertisingCourse> _advertisingCourseRepository;
        private readonly IAsyncRepository<FavoriteCourse> _favoriteCourseRepository;
        private readonly ICheckEdition _checkEdition;

        public CourseAppService(IAsyncRepository<Course> courseRepository,
            IAsyncRepository<GivenCourse> givenCourseRepository,
            IAsyncRepository<AdvertisingCourse> advertisingCourseRepository,
            IAsyncRepository<FavoriteCourse> favoriteCourseRepository,
                ICheckEdition checkEdition
            )
        {
            _courseRepository = courseRepository;
            _givenCourseRepository =givenCourseRepository;
            _advertisingCourseRepository = advertisingCourseRepository;
            _favoriteCourseRepository = favoriteCourseRepository;
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

        public async Task<List<AdvertisingCourseDto>> GetAllAdvertisingCourses()
        {
            var courses = await _advertisingCourseRepository.GetAll().Include(x => x.Course)
                .ThenInclude(x => x.Owners).ThenInclude(x => x.Tenant)
                .Include(x => x.Course)
                .ThenInclude(x => x.Owners).ThenInclude(x => x.Educator)
                .Include(x => x.Course).ThenInclude(x => x.Category)
                .Include(x => x.Tenant)
                .Include(x => x.Educator)
                .Where(x=>x.IsEnded==false && x.Course.AdvertisingState!=AdvertisingState.Stopped)
                .Select(x => new AdvertisingCourseDto
                {
                    CourseInfo = new CourseDto
                    {
                        Id = x.Course.Id,
                        Title = x.Course.Title,
                        Description = x.Course.Description,
                        Quota = x.Course.Quota,
                        Price = x.Course.Price,
                        StartDate = x.Course.StartDate,
                        EndDate = x.Course.EndDate,
                        Score = x.Course.Score,
                        Category = new CategoryDto
                        {
                            Id = x.Course.Category.Id,
                            Description = x.Course.Category.Description,
                            DisplayName = x.Course.Category.DisplayName,
                            ParentCategory = new ParentCategoryDto()
                        },
                        Tenants = x.Course.Owners.Select(t => new CourseTenantDto
                        {
                            TenantId = t.Tenant.Id,
                            TenantName = t.Tenant.TenantName,
                            LogoPath = t.Tenant.LogoPath
                        }).ToList(),
                        Educators = x.Course.Owners.Select(e => new CourseEducatorDto
                        {
                            EducatorId = e.Educator.Id,
                            EducatorName = e.Educator.Name,
                            Profession = e.Educator.Profession,
                            ProfileImgPath = e.Educator.ProfileImagePath
                        }).ToList(),
                    },
                    OwnerEducator = new CourseEducatorDto
                    {
                        EducatorId = x.Educator.Id,
                        EducatorName = x.Educator.Name + " " + x.Educator.Surname,
                        Profession = x.Educator.Profession,
                        ProfileImgPath = x.Educator.ProfileImagePath
                    },
                    OwnerTenant = new CourseTenantDto
                    {
                        TenantId = x.Tenant.Id,
                        TenantName = x.Tenant.TenantName,
                        LogoPath = x.Tenant.LogoPath
                    }
                }).ToListAsync();
            return courses;
        }

        public async Task<Course> CreateAdvertisingCourse(CreateAdvertisingCourseDto input)
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
                AdvertisingState = AdvertisingState.Continues
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

            var advertisingModel = new AdvertisingCourse
            {
                CourseId = course.Id,
                TenantId = input.AdvertisingInfo.OwnerTenantId,
                EducatorId = input.AdvertisingInfo.OwnerEducatorId,
                Price = input.AdvertisingInfo.Price,
                StartDateTime = input.AdvertisingInfo.StartDateTime,
                EndDateTime = input.AdvertisingInfo.EndDateTime
            };
            await _advertisingCourseRepository.AddAsync(advertisingModel);

            return course;
        }

        public async Task AddFavoriteCourse(CreateFavoriteCourseDto input)
        {
            var model = new FavoriteCourse
            {
                CourseId = input.CourseId,
                UserId = input.UserId
            };
            await _favoriteCourseRepository.AddAsync(model);
        }

        public async Task<PagedResultDto<FavoriteCourseDto>> GetFavoriteCourses(long userId)
        {
            var courses = await _favoriteCourseRepository.GetAll().Where(x => x.UserId == userId)
                .Include(x => x.Course)
                .ThenInclude(x => x.Owners).ThenInclude(x => x.Educator)
                .Include(x => x.User)
                .Select(x => new FavoriteCourseDto
                {
                    CourseId = x.Course.Id,
                    CourseName = x.Course.Title,
                    Tenants = x.Course.Owners.Select(t=> new CourseTenantDto
                    {
                        TenantId = t.Tenant.Id,
                        LogoPath = t.Tenant.LogoPath,
                        TenantName = t.Tenant.TenantName
                    }).ToList(),
                    Educators = x.Course.Owners.Select(e=>new CourseEducatorDto
                    {
                        EducatorId = e.Educator.Id,
                        EducatorName=e.Educator.Name,
                        Profession = e.Educator.Profession,
                        ProfileImgPath = e.Educator.ProfileImagePath
                    }).ToList()
                }).ToListAsync();
            return new PagedResultDto<FavoriteCourseDto>
            {
                Results = courses,
                Count = courses.Count
            };
        }
        
        public async Task<PagedResultDto<CourseDto>> GetCoursesByName(string courseName)
        {
            var courses = await _courseRepository.GetAll().Include(x=>x.Category)
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
                Score = x.Score,
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
            var result = new PagedResultDto<CourseDto>
            {
                Results = courses,
                Count = courses.Count
            };
            return result;
        }

        public async Task<PagedResultDto<CourseDto>> GetCoursesByCategory(long categoryId)
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
                    Score = x.Score,
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
            var result = new PagedResultDto<CourseDto>
            {
                Results = courses,
                Count = courses.Count
            };
            return result;
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

        public async Task<PagedResultDto<CourseDto>> GetAllCourses()
        {
            var courses = await _courseRepository.GetAll().Where(x => x.IsDeleted == false && x.AdvertisingState != AdvertisingState.Stopped)
                .Include(x => x.Owners).ThenInclude(x => x.Tenant)
                .Include(x=>x.Owners).ThenInclude(x=>x.Educator)
                .Include(x => x.Category)
                .Include(x=>x.Comments)
                .Select(x => new CourseDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Quota = x.Quota,
                    Price = x.Price,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Score = x.Score,
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
                    }).ToList(),
                }).ToListAsync();
            var result = new PagedResultDto<CourseDto>
            {
                Results = courses,
                Count = courses.Count
            };
            return result;
        }
        
    }
}