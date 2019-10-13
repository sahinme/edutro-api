using System;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.CourseService.Dto
{
    public class CreateCourseDto
    {
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public int Quota { get; set; }
        
        public double Price { get; set; }
        
        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public long CategoryId { get; set; }

        public long[] EducatorId { get; set; }

        public long[] TenantId { get; set; }
    }
}