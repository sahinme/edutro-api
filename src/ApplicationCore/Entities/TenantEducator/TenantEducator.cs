using Microsoft.EgitimAPI.ApplicationCore.Entities.Educators;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;

namespace Microsoft.EgitimAPI.ApplicationCore.Entities.TenantEducator
{
    public class TenantEducator:BaseEntity,IAggregateRoot
    {
        public long StudentId { get; set; }
        public Tenant Tenant { get; set; }

        public long CourseId { get; set; }
        public Educator Educator { get; set; }
    }
}