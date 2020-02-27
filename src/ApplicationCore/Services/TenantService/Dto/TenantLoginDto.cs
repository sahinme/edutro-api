using Microsoft.EgitimAPI.ApplicationCore.Entities;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.TenantService.Dto
{
    public class TenantLoginDto
    {
        public long Id { get; set; }
        public string TenantName { get; set; }
        public EntityType EntityType { get; set; }
    }
}