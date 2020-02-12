using System;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.CourseService.Dto
{
    public class UpdateCourseDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal DiscountPrice { get; set; }
        public string Duration { get; set; }
        public bool Certificate { get; set; }
        public bool CertificateOfParticipation { get; set; }
        public bool OnlineVideo { get; set; }
        public string Requirements { get; set; }
        public string Teachings { get; set; }
        public string Address { get; set; }
        public int Quota { get; set; }
        public decimal Price { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long CategoryId { get; set; }
        public long[] EducatorId { get; set; }
        public long[] TenantId { get; set; }
    }
}