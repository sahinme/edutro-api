using System.Threading.Tasks;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;
using Microsoft.EgitimAPI.ApplicationCore.Services.Category;
using Microsoft.EgitimAPI.ApplicationCore.Services.Category.Dto;

namespace  Microsoft.EgitimAPI.ApplicationCore.Services.CategoryService
{
    public class CategoryAppService:ICategoryAppService
    {
        private readonly IAsyncRepository<Entities.Categories.Category> _categoryRepository;

        public CategoryAppService(IAsyncRepository<Entities.Categories.Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        
        public async Task CreateCategory(CreateCategoryDto input)
        {
            var category = new Entities.Categories.Category
            {
                DisplayName = input.DisplayName,
                ParentCategoryId = input.ParentCategoryId,
                Description = input.Description
            };
            await _categoryRepository.AddAsync(category);
        }
    }
}