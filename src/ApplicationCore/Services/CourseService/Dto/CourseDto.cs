using System;
using System.Collections.Generic;
using Microsoft.EgitimAPI.ApplicationCore.Services.Category.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.TenantService.Dto;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.CourseService.Dto
{
    public class CourseDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public int Quota { get; set; }    
        
        public double Price { get; set; }
        
        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public CategoryDto Category { get; set; }
        
        public List<TenantDto> Tenants { get; set; }
    }
}