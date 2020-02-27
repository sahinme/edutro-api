namespace Microsoft.EgitimAPI.ApplicationCore.Services.CourseService.Dto
{
    public class GetCourseDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
    }
}