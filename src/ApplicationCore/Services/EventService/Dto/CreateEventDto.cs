using System;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Events;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.EventService.Dto
{
    public class CreateEventDto
    {
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public int Quota { get; set; }

        public string Address { get; set; }

        public long LocationId { get; set; }
        public long OwnerId { get; set; }
        public string OwnerType { get; set; }
        
        public double Price { get; set; }
        
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public EventType EventType { get; set; }
        public long CategoryId { get; set; }

        public long[] EducatorId { get; set; }

        public long[] TenantId { get; set; }
    }
}