using System;
using System.Collections.Generic;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Events;
using Microsoft.EgitimAPI.ApplicationCore.Services.Category.Dto;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.EventService.Dto
{
    public class EventDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public int Quota { get; set; }

        public List<OwnerInfo> OwnerInfo { get; set; }
        public double Price { get; set; }
        public string OwnerType { get; set; }
        public string Address { get; set; }
        public string LocationName { get; set; }
        public EventType EventType { get; set; }
        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public CategoryDto Category { get; set; }
        
        public List<EventTenantDto> Tenants { get; set; }
        
        public List<EventEducatorDto> Educators { get; set; }
    }
    
    public class EventTenantDto
    {
        public long? TenantId { get; set; }

        public string TenantName { get; set; }

        public string LogoPath { get; set; }
    }

    public class EventEducatorDto
    {
        public long? EducatorId { get; set; }

        public string EducatorName { get; set; }

        public string ProfileImgPath { get; set; }

        public string Profession { get; set; }
    }

    public class OwnerInfo
    {
        public string Name { get; set; }
        public long Id { get; set; }
        public string LogoPath { get; set; }
        public string Profession { get; set; }
    }
}