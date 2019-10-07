using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Educators;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;
using Microsoft.EgitimAPI.ApplicationCore.Services.EducatorService.Dto;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.EducatorService
{
    public class EducatorAppService:IEducatorAppService
    {
        private readonly IAsyncRepository<Educator> _educatorRepository;

        public EducatorAppService(IAsyncRepository<Educator> educatorRepository)
        {
            _educatorRepository = educatorRepository;
        }
        
        public async Task CreateEducator(CreateEducatorDto input)
        {
            var educator = new Educator
            {
                Name = input.Name,
                Surname = input.Surname,
                Profession = input.Profession,
                Resume = input.Resume,
                TenantId = input.TenantId
            };
            await _educatorRepository.AddAsync(educator);
        }

        public async Task<List<EducatorDto>> GetAllEducators()
        {
            var educators = await _educatorRepository.GetAll().Include(x => x.Tenant)
                .Select(x => new EducatorDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    Profession = x.Profession,
                    Resume = x.Resume,
                    TenantName = x.Tenant.TenantName,
                    TenantId = x.Tenant.Id
                }).ToListAsync();
            return educators;
        }
    }
}