using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Categories;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;

namespace Microsoft.EgitimAPI.ApplicationCore.Entities.Courses
{
    public class Course:BaseEntity,IAggregateRoot
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Quota { get; set; }
        public double Price { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long CategoryId { get; set; }
        public long? CourseContentId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }        
        
        [ForeignKey(nameof(CourseContentId))]
        public virtual CourseContent CourseContent { get; set; }
        
        public IList<GivenCourse> Tenants { get; set; }
        
       // public IList<GivenCourse> Educators { get; set; }
        
        //public IList<GivenCourse> Educators { get; set; }
    }
}