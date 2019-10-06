using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;

namespace Microsoft.EgitimAPI.ApplicationCore.Entities.Educators
{
    public class Educator:BaseEntity,IAggregateRoot
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Profession { get; set; }

        //Kisa ozgecmis bilgileri
        public string Resume { get; set; }

        public string ProfileImagePath { get; set; }
        public long? TenantId { get; set; }
        
        [ForeignKey(nameof(TenantId))]
        public virtual Tenant Tenant { get; set; }
    }
}