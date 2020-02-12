using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Comments;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Courses;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Editions;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Events;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Notifications;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;

namespace Microsoft.EgitimAPI.ApplicationCore.Entities.Educators
{
    public class Educator:BaseEntity,IAggregateRoot
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public float Score { get; set; }
        public string Profession { get; set; }
        //Kisa ozgecmis bilgileri
        public string Resume { get; set; }
        public string ProfileImagePath { get; set; }
        [DefaultValue(false)]
        public bool IsPremium { get; set; }
        public IList<TenantEducator.TenantEducator> EducatorTenants { get; set; }
        public virtual IList<GivenCourse> GivenCourses { get; set; }
        public  IList<GivenEvent> GivenEvents { get; set; }
        public IList<AdvertisingCourse> AdvertisingCourses { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}