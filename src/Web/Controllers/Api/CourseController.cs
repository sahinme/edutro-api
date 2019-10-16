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
                var course =  await _courseAppService.CreateCourse(input);
                return Ok(course);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateAdvertisingCourse(CreateAdvertisingCourseDto input)
        {
            try
            {
                var course = await _courseAppService.CreateAdvertisingCourse(input);
                return Ok(course);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        
        [HttpGet]
        public async Task<IActionResult> GetCoursesByName(string courseName)
        {
            var courses =  await _courseAppService.GetCoursesByName(courseName);
            return Ok(courses);
        }

        [HttpGet]
        public async Task<IActionResult> GetAdvertisingCourses()
        {
            var courses = await _courseAppService.GetAllAdvertisingCourses();
            return Ok(courses);
        }

        [HttpGet]
        public async Task<IActionResult> GetCoursesByCategory(long categoryId)
        {
            var courses = await _courseAppService.GetCoursesByCategory(categoryId);
            return Ok(courses);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var result = await _courseAppService.GetAllCourses();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddFavoriteCourse(CreateFavoriteCourseDto input)
        {
            await _courseAppService.AddFavoriteCourse(input);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetFavoriteCourses(long id)
        {
            var result = await _courseAppService.GetFavoriteCourses(id);
            return Ok(result);
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