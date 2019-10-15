using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Educators;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;

namespace Microsoft.EgitimAPI.ApplicationCore.Entities.Courses
{
    public class AdvertisingCourse:BaseEntity,IAggregateRoot
    {
        public float Price { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public bool IsEnded { get; set; }
        public long CourseId { get; set; }
        public long? TenantId { get; set; }
        public long? EducatorId { get; set; }
        
        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }
        
        [ForeignKey(nameof(TenantId))]
        public Tenant Tenant { get; set; }
        
        [ForeignKey(nameof(EducatorId))]
        public Educator Educator { get; set; }
    }
}