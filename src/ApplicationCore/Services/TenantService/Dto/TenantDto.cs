using System.Collections.Generic;
using Microsoft.EgitimAPI.ApplicationCore.Entities;
using Microsoft.EgitimAPI.ApplicationCore.Services.CourseService.Dto;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.TenantService.Dto
{
    public class TenantDto:BaseEntity
    {
        public bool IsPremium { get; set; }

        public string TenantName { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string PhoneNumber2 { get; set; }

        public string LogoPath { get; set; } 
        
        public List<TenantEducatorDto> TenantEducators { get; set; }
        
        public List<CourseDto> Courses { get; set; }
    }

    public class TenantEducatorDto
    {
        public long EducatorId { get; set; }

        public string EducatorName { get; set; }

        public string Profession { get; set; }
    }
}