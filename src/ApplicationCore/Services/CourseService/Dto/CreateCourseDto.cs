using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Courses;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.CourseService.Dto
{
    public class CreateCourseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Quota { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public string Duration { get; set; }
        public bool Certificate { get; set; }
        public bool CertificateOfParticipation { get; set; }
        public bool OnlineVideo { get; set; }
        public string Requirements { get; set; }
        public string Teachings { get; set; }
        public string Address { get; set; }
        public long LocationId { get; set; }
        public string OwnerType { get; set; }
        public long OwnerId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long CategoryId { get; set; }
        public long[] EducatorId { get; set; }
        public long[] TenantId { get; set; }
        public IFormFile File { get; set; }
    }
}