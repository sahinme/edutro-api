using System;
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
        public async Task<IActionResult> CreateTenant(CreateTenantDto input)
        {
            try
            {
                await _tenantAppService.CreateTenant(input);
                return Ok("Created Tenant");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [Authorize]
        [HttpGet]
        public  List<TenantDto> GetAll()
        {
            return  _tenantAppService.GetAll();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTenant(long id)
        {
            try
            {
                await _tenantAppService.Delete(id);
                return Ok("Deleted tenant");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}