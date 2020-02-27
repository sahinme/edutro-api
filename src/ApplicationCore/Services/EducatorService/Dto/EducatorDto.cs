using System.Collections.Generic;
using Microsoft.EgitimAPI.ApplicationCore.Services.CourseService.Dto;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.EducatorService.Dto
{
    public class EducatorDto
    {
        public long Id { get; set; }    
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Profession { get; set; }
        //Kisa ozgecmis bilgileri
        public string Resume { get; set; }
        public string ProfileImagePath { get; set; }
        public float Score { get; set; }
        public List<EducatorTenantDto> EducatorTenants { get; set; }
        public List<CourseDto> Courses { get; set; }
    }

    public class EducatorTenantDto
    {
        public long? TenantId { get; set; }

        public string TenantName { get; set; }
    }
}