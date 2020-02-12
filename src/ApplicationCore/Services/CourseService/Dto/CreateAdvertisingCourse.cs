using System;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.CourseService.Dto
{
    public class CreateAdvertisingCourseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public long LocationId { get; set; }
        public long OwnerId { get; set; }
        public string OwnerType { get; set; }
        public int Quota { get; set; }
        public decimal Price { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long CategoryId { get; set; }
        public long[] EducatorId { get; set; }
        public long[] TenantId { get; set; }
        public AdvertisingDto AdvertisingInfo { get; set; }
    }
}