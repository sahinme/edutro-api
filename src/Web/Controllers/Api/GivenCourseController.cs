using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EgitimAPI.ApplicationCore.Services.GivenCourseService;
using Microsoft.EgitimAPI.ApplicationCore.Services.GivenCourseService.Dto;

namespace Microsoft.EgitimAPI.Web.Controllers.Api
{
    public class GivenCourseController:BaseApiController
    {
        private readonly IGivenCourseAppService _givenCourseAppService;

        public GivenCourseController(IGivenCourseAppService givenCourseAppService)
        {
            _givenCourseAppService = givenCourseAppService;
        }
        
        [HttpGet]
        public async Task<List<GivenCourseDto>> GetAllGivenCourses()
        {
            return await _givenCourseAppService.GetAllGivenCourses();
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateGivenCourse(CreateGivenCourseDto input)
        {
            await _givenCourseAppService.CreateGivenCourse(input);
            return Ok("Created given course");
        }
    }
}