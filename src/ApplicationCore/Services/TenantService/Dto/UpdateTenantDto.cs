using Microsoft.AspNetCore.Http;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.TenantService.Dto
{
    public class UpdateTenantDto
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string TenantName { get; set; }
        public string Address { get; set; }
        public long LocationId { get; set; }
        public string AboutUs { get; set; }
        public string Title { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumber2 { get; set; }
        public IFormFile LogoFile { get; set; } 
    }
}