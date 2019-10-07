using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Educators;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;

namespace Microsoft.EgitimAPI.ApplicationCore.Entities.Courses
{
    public class GivenCourse:BaseEntity,IAggregateRoot
    {
        public long CourseId { get; set; }
        
        public long? EducatorId { get; set; }

        public long? TenantId { get; set; }
        
        [ForeignKey(nameof(CourseId))]
        public virtual Course Course { get; set; }
        
        [ForeignKey(nameof(EducatorId))]
        public virtual  Educator Educator { get; set; }
        
        [ForeignKey(nameof(TenantId))]
        public virtual  Tenant Tenant { get; set; }
    }
}