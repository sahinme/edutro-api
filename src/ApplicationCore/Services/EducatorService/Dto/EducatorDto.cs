using System.Collections.Generic;

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

        public List<EducatorTenantDto> EducatorTenants { get; set; }
    }

    public class EducatorTenantDto
    {
        public long? TenantId { get; set; }

        public string TenantName { get; set; }
    }
}