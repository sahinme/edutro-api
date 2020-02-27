using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Categories;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Comments;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Locations;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;

namespace Microsoft.EgitimAPI.ApplicationCore.Entities.Courses
{
    public class Course:BaseEntity,IAggregateRoot
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Quota { get; set; }
        public long LocationId { get; set; }
        public string ImagePath { get; set; }
        public string Address { get; set; }
        public long OwnerId { get; set; }
        public string OwnerType { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public float Score { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Duration { get; set; }
        public bool Certificate { get; set; }
        public bool CertificateOfParticipation { get; set; }
        public bool OnlineVideo { get; set; }
        public string Requirements { get; set; }
        public string ShortDescription { get; set; }
        public string Teachings { get; set; }
        public long CategoryId { get; set; }
        public AdvertisingState AdvertisingState { get; set; }
        public long? CourseContentId { get; set; }
        
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }     
        
        [ForeignKey(nameof(LocationId))]
        public virtual Location Location { get; set; }     
        
        [ForeignKey(nameof(CourseContentId))]
        public virtual CourseContent CourseContent { get; set; }
        public  IList<GivenCourse> Owners { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}