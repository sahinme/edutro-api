using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Categories;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Locations;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;

namespace Microsoft.EgitimAPI.ApplicationCore.Entities.Events
{
    public class Event:BaseEntity,IAggregateRoot
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public DateTime? StartDate { get; set; }
        public long OwnerId { get; set; }
        public string OwnerType { get; set; }
        public long LocationId { get; set; }
        public DateTime? EndDate { get; set; }
        public long CategoryId { get; set; }
        public int Quota { get; set; }
        public double Price { get; set; }
        public EventType EventType { get; set; }
        
        [ForeignKey(nameof(LocationId))]
        public virtual Location Location { get; set; }  
        public virtual Category Category { get; set; }      
        public  IList<GivenEvent> Owners { get; set; }
    }
}