using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EgitimAPI.ApplicationCore.Services.Category.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.CourseService.Dto;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.Category
{
    public interface ICategoryAppService
    {
        Task CreateCategory(CreateCategoryDto input);
        Task<List<CategoryDto>> GetAllCategories();
    }
}