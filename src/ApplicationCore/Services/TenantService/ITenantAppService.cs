using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EgitimAPI.ApplicationCore.Services.TenantService.Dto;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.TenantService
{
    public interface ITenantAppService
    {
        Task CreateTenant(CreateTenantDto input);
        
        Task<TenantDto> GetTenantById(long id);

        List<TenantDto> GetAll();
    }
}