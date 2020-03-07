using System.Collections.Generic;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.TenantService.Dto
{
    public class TenantEducatorListDto
    {
        public List<TenantEducatorsDto> TenantEducators { get; set; }
    }
    public class TenantEducatorsDto
    {
        public long Id { get; set; }    
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Profession { get; set; }
        public string ProfileImagePath { get; set; }
        public bool IsAccepted { get; set; }
    }
}