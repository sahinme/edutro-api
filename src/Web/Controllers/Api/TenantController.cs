using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EgitimAPI.ApplicationCore.Services.TenantService;
using Microsoft.EgitimAPI.ApplicationCore.Services.TenantService.Dto;

namespace Microsoft.EgitimAPI.Web.Controllers.Api
{
    public class TenantController:BaseApiController
    {
        private readonly ITenantAppService _tenantAppService;

        public TenantController(ITenantAppService tenantAppService)
        {
            _tenantAppService = tenantAppService;
        }
        
        [HttpGet]
        public async Task<TenantDto> GetById(long id)
        {
            return await _tenantAppService.GetTenantById(id);
        }
        
        [HttpPost]
        public IActionResult CreateTenant(CreateTenantDto input)
        {
            var tenant = _tenantAppService.CreateTenant(input);
            return Ok(tenant);
        }
        
        [Authorize]
        [HttpGet]
        public  List<TenantDto> GetAll()
        {
            return  _tenantAppService.GetAll();
        }
    }
}