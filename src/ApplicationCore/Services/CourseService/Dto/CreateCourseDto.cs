using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EgitimAPI.ApplicationCore.Entities;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Courses;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Posts;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.CourseService.Dto
{
    public class CreateCourseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public int Quota { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public string DurationType { get; set; }
        public int DurationCount { get; set; }
        public PostState CourseState { get; set; }
        public bool Certificate { get; set; }
        public bool CertificateOfParticipation { get; set; }
        public bool OnlineVideo { get; set; }
        public string Requirements { get; set; }
        public string Teachings { get; set; }
        public string Address { get; set; }
        public long LocationId { get; set; }
        public EntityType OwnerType { get; set; }
        public long OwnerId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long CategoryId { get; set; }
        public long[] EducatorId { get; set; }
        public long[] TenantId { get; set; }
        public IFormFile File { get; set; }
    }
}