using System.Collections.Generic;
using System.Threading.Tasks;
using EgitimAPI.ApplicationCore.Services.UserService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Entities;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Courses;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Users;
using Microsoft.EgitimAPI.ApplicationCore.Services.CourseService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.Dto;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.CourseService
{
    public interface ICourseAppService
    {
        Task<Course> CreateCourse(CreateCourseDto input);
        Task<PagedResultDto<CourseDto>> GetCoursesByName(string courseName);
        Task<PagedResultDto<GetCourseDto>> GetEntityCourses(EntityType entityType,long id);
        Task<PagedResultDto<CourseDto>> SearchCourses(string query,long locationId);
        Task<PagedResultDto<CourseDto>> GetCoursesByCategory(long categoryId);
        Task DeleteCourse(long id);
        Task SuspendOrActivateCourse(long id);
        Task UpdateCourse(UpdateCourseDto course);
        Task<PagedResultDto<CourseDto>> GetAllCourses();
        Task<CourseDto> GetCourseById(long id);
        Task<PagedResultDto<AdvertisingCourseDto>> GetAllAdvertisingCourses();
        Task<Course> CreateAdvertisingCourse(CreateAdvertisingCourseDto input);
        Task AddFavoriteCourse(CreateFavoriteCourseDto input);
        Task<PagedResultDto<FavoriteCourseDto>> GetFavoriteCourses(long userId);
    }
}