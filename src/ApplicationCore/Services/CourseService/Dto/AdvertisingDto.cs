using System;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.CourseService.Dto
{
    public class AdvertisingDto
    {
        public float Price { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public long? OwnerTenantId { get; set; }
        public long? OwnerEducatorId { get; set; }
    }
}