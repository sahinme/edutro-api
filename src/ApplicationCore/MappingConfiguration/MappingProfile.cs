using AutoMapper;
using EgitimAPI.ApplicationCore.Services.UserService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants;
using Microsoft.EgitimAPI.ApplicationCore.Services.TenantService.Dto;

namespace Microsoft.EgitimAPI.MappingConfiguration
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateTenantDto,Tenant>();
            CreateMap<Tenant,CreateUserDto>();
            CreateMap<TenantDto, Tenant>();
            CreateMap<Tenant, TenantDto>();
        }
    }
}