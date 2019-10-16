using System.Collections.Generic;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.CourseService.Dto
{
    public class FavoriteCourseDto
    {
        public long CourseId { get; set; }
        public string CourseName { get; set; }
        public List<CourseTenantDto> Tenants { get; set; }
        public List<CourseEducatorDto> Educators { get; set; }
        
    }
    
}