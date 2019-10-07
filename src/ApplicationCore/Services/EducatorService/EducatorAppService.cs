using System.Threading.Tasks;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Educators;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;
using Microsoft.EgitimAPI.ApplicationCore.Services.EducatorService.Dto;

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
    }
}