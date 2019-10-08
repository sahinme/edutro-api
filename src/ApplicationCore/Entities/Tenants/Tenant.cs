using System.Collections.Generic;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Educators;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;

namespace Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants
{
    public class Tenant:BaseEntity,IAggregateRoot
    {
        public bool IsPremium { get; set; }

        public string Password { get; set; }
        
        public string TenantName { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string PhoneNumber2 { get; set; }

        public string LogoPath { get; set; }    
        
        public ICollection<Educator> Educators { get; set; }
    }
}