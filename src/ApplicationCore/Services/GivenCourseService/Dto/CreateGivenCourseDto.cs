namespace Microsoft.EgitimAPI.ApplicationCore.Services.GivenCourseService.Dto
{
    public class CreateGivenCourseDto
    {
        public long CourseId { get; set; }

        public long? TenantId { get; set; }

        public long? EducatorId { get; set; }
    }
}