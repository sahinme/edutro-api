using System.Collections.Generic;
using System.Threading.Tasks;
using EgitimAPI.ApplicationCore.Services.UserService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.TenantService.Dto;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.TenantService
{
    public interface ITenantAppService
    {
        Task CreateTenant(CreateTenantDto input);
        Task<TenantDto> GetTenantById(long id);
        List<TenantDto> GetAll();
        Task Delete(long id);
        Task AddEducator(CreateTenantEducatorDto input);
        Task<List<TenantEducatorListDto>> GetEducators(long id);
        Task<TenantLoginDto> Login(TenantOrEducatorLoginDto input);

    }
}