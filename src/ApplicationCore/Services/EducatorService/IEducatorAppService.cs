using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Educators;
using Microsoft.EgitimAPI.ApplicationCore.Services.EducatorService.Dto;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.EducatorService
{
    public interface IEducatorAppService
    {
        Task<Educator> CreateEducator(CreateEducatorDto input);
        Task<List<EducatorDto>> GetAllEducators();
        Task Delete(long id);
        Task<Educator> UpdateEducator(UpdateEducatorDto input);
        Task<List<EducatorDto>> GetEducatorByName(string educatorName);
        Task<EducatorDto> GetEducatorById(long educatorId);
        Task HandleSubscribeTenant(SubscribeDto input);
    }
}