namespace Microsoft.EgitimAPI.ApplicationCore.Services.Category.Dto
{
    public class CategoryDto
    {
        public long Id { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public ParentCategoryDto ParentCategory { get; set; }
    }

    public class ParentCategoryDto
    {
        public long Id { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
    }
}