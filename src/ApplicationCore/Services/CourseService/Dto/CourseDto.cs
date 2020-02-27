using System;
using System.Collections.Generic;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Comments;
using Microsoft.EgitimAPI.ApplicationCore.Services.Category.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.EducatorService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.TenantService.Dto;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.CourseService.Dto
{
    public class CourseDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal DiscountPrice { get; set; }
        public string Duration { get; set; }
        public bool Certificate { get; set; }
        public bool CertificateOfParticipation { get; set; }
        public bool OnlineVideo { get; set; }
        public string Requirements { get; set; }
        public string Teachings { get; set; }
        public string Address { get; set; }
        public int Quota { get; set; }    
        public decimal Price { get; set; }
        public string LocationName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ImagePath { get; set; }
        public List<CourseOwnerInfo> CourseOwnerInfo { get; set; }
        public CategoryDto Category { get; set; }
        public float Score { get; set; }
        public List<CourseTenantDto> Tenants { get; set; }
        public List<CourseEducatorDto> Educators { get; set; }
    }

    public class CourseTenantDto
    {
        public long? TenantId { get; set; }

        public string TenantName { get; set; }

        public string LogoPath { get; set; }
    }

    public class CourseEducatorDto
    {
        public long? EducatorId { get; set; }

        public string EducatorName { get; set; }

        public string ProfileImgPath { get; set; }

        public string Profession { get; set; }
    }
    
    public class CourseOwnerInfo
    {
        public string EntityType { get; set; }
        public string Name { get; set; }
        public long Id { get; set; }
        public string LogoPath { get; set; }
        public string Profession { get; set; }
    }
}