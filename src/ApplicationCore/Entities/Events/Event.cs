using System;
using System.Collections.Generic;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Categories;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;

namespace Microsoft.EgitimAPI.ApplicationCore.Entities.Events
{
    public class Event:BaseEntity,IAggregateRoot
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long CategoryId { get; set; }
        public int Quota { get; set; }
        public double Price { get; set; }
        public EventType EventType { get; set; }
        public virtual Category Category { get; set; }      
        public  IList<GivenEvent> Owners { get; set; }
    }
}