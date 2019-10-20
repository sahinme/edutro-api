using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EgitimAPI.ApplicationCore.Services.GivenCourseService.Dto;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.GivenCourseService
{
    public interface IGivenCourseAppService
    {
        Task<List<GivenCourseDto>> GetAllGivenCourses();

        Task CreateGivenCourse(CreateGivenCourseDto input);
        Task GetGiven();
    }
}