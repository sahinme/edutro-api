using System.Collections.Generic;
using System.Threading.Tasks;
using EgitimAPI.ApplicationCore.Services.UserService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Users;
using Microsoft.EgitimAPI.ApplicationCore.Services.CourseService.Dto;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.CourseService
{
    public interface ICourseAppService
    {
        Task CreateCourse(CreateCourseDto input);
        Task<List<CourseDto>> GetCoursesByName(string courseName);
        Task<List<CourseDto>> GetCoursesByCategory(long categoryId);
        Task DeleteCourse(long id);
        Task UpdateCourse(UpdateCourseDto course);
        Task<List<CourseDto>> GetAllCourses();

    }
}