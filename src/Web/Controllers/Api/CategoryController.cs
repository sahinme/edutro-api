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
        public IActionResult CreateCategory(CreateCategoryDto input)
        {
            var category = _categoryAppService.CreateCategory(input);
            return Ok(category);
        }
    }
}