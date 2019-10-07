using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EgitimAPI.ApplicationCore.Services.CourseService;
using Microsoft.EgitimAPI.ApplicationCore.Services.CourseService.Dto;

namespace Microsoft.EgitimAPI.Web.Controllers.Api
{
    public class CourseController:BaseApiController
    {
        private readonly ICourseAppService _courseAppService;

        public CourseController(ICourseAppService courseAppService)
        {
            _courseAppService = courseAppService;
        }
        
        [HttpPost]
        public IActionResult CreateCourse(CreateCourseDto input)
        {
            var tenant = _courseAppService.CreateCourse(input);
            return Ok(tenant);
        }
        
        [HttpGet]
        public async Task<List<CourseDto>> GetCoursesByName(string courseName)
        {
            return await _courseAppService.GetCoursesByName(courseName);
        }
    }
}