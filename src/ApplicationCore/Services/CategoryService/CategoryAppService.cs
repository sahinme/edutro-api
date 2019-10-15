using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;
using Microsoft.EgitimAPI.ApplicationCore.Services.Category;
using Microsoft.EgitimAPI.ApplicationCore.Services.Category.Dto;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<CategoryDto>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAll().Include(x => x.ParentCategory)
                .Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Description = x.Description,    
                    DisplayName = x.DisplayName,
                    ParentCategory = x.ParentCategoryId !=null ? new ParentCategoryDto
                    {
                        Id = x.ParentCategory.Id,
                        Description = x.ParentCategory.Description,
                        DisplayName = x.ParentCategory.DisplayName
                    }:null
                }).ToListAsync();
            return categories;
        }
    }
}