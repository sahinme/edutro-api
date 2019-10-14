using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EgitimAPI.ApplicationCore.Services.EducatorService;
using Microsoft.EgitimAPI.ApplicationCore.Services.EducatorService.Dto;

namespace Microsoft.EgitimAPI.Web.Controllers.Api
{
    public class EducatorController:BaseApiController
    {
        private readonly IEducatorAppService _educatorAppService;

        public EducatorController(IEducatorAppService educatorAppService)
        {
            _educatorAppService = educatorAppService;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateEducator(CreateEducatorDto input)
        {
            try
            {
                var user = await _educatorAppService.CreateEducator(input);
                return Ok(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateEducator(UpdateEducatorDto input)
        {
            try
            {
                var educator = await _educatorAppService.UpdateEducator(input);
                return Ok(educator);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        
        [HttpGet]
        public async Task<List<EducatorDto>> GetAllEducator()
        {
            return await _educatorAppService.GetAllEducators();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEducator(long id)
        {
            try
            {
                await _educatorAppService.Delete(id);
                return Ok("Deleted educator");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}