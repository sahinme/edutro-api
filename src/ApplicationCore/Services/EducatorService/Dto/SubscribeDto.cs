namespace Microsoft.EgitimAPI.ApplicationCore.Services.EducatorService.Dto
{
    public class SubscribeDto
    {
        public long EducatorId { get; set; }
        public long TenantId { get; set; }
        public bool IsAccepted { get; set; }
    }
}