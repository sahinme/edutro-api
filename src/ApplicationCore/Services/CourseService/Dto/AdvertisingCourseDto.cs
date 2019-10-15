namespace Microsoft.EgitimAPI.ApplicationCore.Services.CourseService.Dto
{
    public class AdvertisingCourseDto
    {
        public CourseDto CourseInfo { get; set; }
        public CourseTenantDto OwnerTenant { get; set; }
        public CourseEducatorDto OwnerEducator { get; set; }
    }
}