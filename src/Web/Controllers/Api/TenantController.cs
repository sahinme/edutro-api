using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants;
using Microsoft.EgitimAPI.ApplicationCore.Services.Dto;
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

        [HttpGet("by-id")]
        public async Task<TenantDto> GetDetails(long id)
        {
            return await _tenantAppService.GetTenantById(id);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateTenant([FromForm] CreateTenantDto input)
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
        
        [HttpPut]
        public async Task<Tenant> UpdateTenant([FromForm] UpdateTenantDto input)
        {
            var result = await _tenantAppService.UpdateTenant(input);
            return result;
        }
        
        [HttpGet]
        public  List<TenantDto> GetAll()
        {
            return  _tenantAppService.GetAll();
        }
        
        [HttpGet]
        public async Task<IActionResult> GetEducators(long id)
        {
           var result = await _tenantAppService.GetEducators(id);
           return Ok(result);
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

        [HttpPost]
        public async Task<IActionResult> AddEducator(CreateTenantEducatorDto input)
        {
            await _tenantAppService.AddEducator(input);
            return Ok();
        }
        
        [HttpDelete]
        public async Task<IActionResult> RemoveEducator(CreateTenantEducatorDto input)
        {
            await _tenantAppService.RemoveEducator(input);
            return Ok();
        }
    }
}