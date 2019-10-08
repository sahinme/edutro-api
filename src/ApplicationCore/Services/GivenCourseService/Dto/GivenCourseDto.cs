using Microsoft.EgitimAPI.ApplicationCore.Services.CourseService.Dto;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.GivenCourseService.Dto
{
    public class GivenCourseDto
    {
        public long Id { get; set; }
        public CourseDto Course { get; set; }

        public long? TenantId { get; set; }

        public string TenantName { get; set; }

        public string LogoPath { get; set; }

        public long? EducatorId { get; set; }

        public string EducatorFullName { get; set; }
    }
}