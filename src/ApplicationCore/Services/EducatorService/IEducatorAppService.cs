using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EgitimAPI.ApplicationCore.Services.EducatorService.Dto;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.EducatorService
{
    public interface IEducatorAppService
    {
        Task CreateEducator(CreateEducatorDto input);

        Task<List<EducatorDto>> GetAllEducators();
    }
}