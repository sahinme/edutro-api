using System;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Events;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.EventService.Dto
{
    public class CreateEventDto
    {
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public int Quota { get; set; }
        
        public double Price { get; set; }
        
        public DateTime? StartDate { get; set; }

        public string Location { get; set; }
        public DateTime? EndDate { get; set; }

        public EventType EventType { get; set; }
        public long CategoryId { get; set; }

        public long[] EducatorId { get; set; }

        public long[] TenantId { get; set; }
    }
}