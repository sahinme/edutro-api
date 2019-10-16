using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Comments;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Courses;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Educators;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Events;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Notifications;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;

namespace Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants
{
    public class Tenant:BaseEntity,IAggregateRoot
    {
        public bool IsPremium { get; set; }
        public string Password { get; set; }
        public float Score { get; set; }
        public string TenantName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumber2 { get; set; }
        public string LogoPath { get; set; }
        public IList<TenantEducator.TenantEducator> TenantEducators { get; set; }
        public  IList<GivenCourse> GivenCourses { get; set; }
        
        public  IList<GivenEvent> GivenEvents { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public IList<AdvertisingCourse> AdvertisingCourses { get; set; }
    }
}