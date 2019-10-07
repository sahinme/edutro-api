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
        public IActionResult CreateEducator(CreateEducatorDto input)
        {
            var educator = _educatorAppService.CreateEducator(input);
            return Ok(educator);
        }
    }
}