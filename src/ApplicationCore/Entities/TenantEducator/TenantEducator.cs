using System.ComponentModel;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Educators;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;

namespace Microsoft.EgitimAPI.ApplicationCore.Entities.TenantEducator
{
    public class TenantEducator:BaseEntity,IAggregateRoot
    {
        [DefaultValue(false)]
        public bool IsAccepted { get; set; }
        public long TenantId { get; set; }
        public Tenant Tenant { get; set; }

        public long EducatorId { get; set; }
        public Educator Educator { get; set; }
    }
}