using System;
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
        public async Task<IActionResult> CreateCourse(CreateCourseDto input)
        {
            try
            {
                 await _courseAppService.CreateCourse(input);
                return Ok("created Course");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        
        [HttpGet]
        public async Task<List<CourseDto>> GetCoursesByName(string courseName)
        {
            return await _courseAppService.GetCoursesByName(courseName);
        }
        
        [HttpGet]
        public async Task<List<CourseDto>> GetAllCourses()
        {
            return await _courseAppService.GetAllCourses();
        }
        
        [HttpDelete]
        public async Task DeleteCourse(long courseId)
        {
            try
            {
                await _courseAppService.DeleteCourse(courseId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpPut]
        public async Task UpdateCourse(UpdateCourseDto input)
        {
            try
            {
                await _courseAppService.UpdateCourse(input);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}