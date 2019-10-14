using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Educators;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;

namespace Microsoft.EgitimAPI.ApplicationCore.Entities.Events
{
    public class GivenEvent:BaseEntity,IAggregateRoot
    {
        public long EventId { get; set; }
        public long? TenantId { get; set; }

        public long? EducatorId { get; set; }
        
        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; }
        
        [ForeignKey(nameof(TenantId))]
        public Tenant Tenant { get; set; }
        
        [ForeignKey(nameof(EducatorId))]
        public Educator Educator { get; set; }
    }
}