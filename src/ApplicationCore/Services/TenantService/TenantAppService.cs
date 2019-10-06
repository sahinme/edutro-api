using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;
using Microsoft.EgitimAPI.ApplicationCore.Services.TenantService.Dto;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.TenantService
{
    public class TenantAppService:ITenantAppService
    {
        private readonly IAsyncRepository<Tenant> _tenantRepository;
        private readonly IMapper _mapper;

        public TenantAppService(IAsyncRepository<Tenant> tenantRepository,IMapper mapper)
        {
            _tenantRepository = tenantRepository;
            _mapper = mapper;
        }
        
        public async Task CreateTenant(CreateTenantDto input)
        {
            //var user = _mapper.Map<Tenant>(input);  
            var user = new Tenant
            {
                TenantName = input.TenantName,
                Address = input.Address,
                IsPremium = input.IsPremium,
                PhoneNumber = input.PhoneNumber,
                PhoneNumber2 = input.PhoneNumber2,
                Password = input.Password
            };
            await _tenantRepository.AddAsync(user);
        }

        public async Task<TenantDto> GetTenantById(long id)
        {
            var tenat = await _tenantRepository.GetByIdAsync(id);
            var tenatModel = new TenantDto
            {
                Id = tenat.Id,
                TenantName = tenat.TenantName,
                Address = tenat.Address,
                IsPremium = tenat.IsPremium,
                PhoneNumber = tenat.PhoneNumber,
                PhoneNumber2 = tenat.PhoneNumber2,
            };
            return tenatModel;
        }

        
        public List<TenantDto> GetAll()
        {
            var model = _tenantRepository.GetAll().Where(x => x.IsDeleted == false).Select(x => new TenantDto
            {
                Id = x.Id,
                TenantName = x.TenantName,
                PhoneNumber = x.PhoneNumber,
                PhoneNumber2 = x.PhoneNumber2,
                LogoPath = x.LogoPath,
                IsPremium = x.IsPremium,
                Address = x.Address
            }).ToList();
            
            return model;
        }
    }
}