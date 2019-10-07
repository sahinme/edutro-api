namespace Microsoft.EgitimAPI.ApplicationCore.Services.Category.Dto
{
    public class CreateCategoryDto
    {
        public string DisplayName { get; set; }

        public string Description { get; set; }
        
        public long? ParentCategoryId { get; set; }

    }
}