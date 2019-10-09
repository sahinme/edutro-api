using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EgitimAPI.ApplicationCore.Services.Category;
using Microsoft.EgitimAPI.ApplicationCore.Services.Category.Dto;

namespace Microsoft.EgitimAPI.Web.Controllers.Api
{
    public class CategoryController:BaseApiController
    {
        private readonly ICategoryAppService _categoryAppService;

        public CategoryController(ICategoryAppService categoryAppService)
        {
            _categoryAppService = categoryAppService;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto input)
        {
            try
            { 
                await _categoryAppService.CreateCategory(input);
                return Ok("Created category");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
          
        }
    }
}