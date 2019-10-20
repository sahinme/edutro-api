using Microsoft.EgitimAPI.ApplicationCore.Entities.Notifications;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.NotificationService.Dto
{
    public class NotificationDto
    {
        public string Title { get; set; }
        public long ContentId { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public NotifyContentType NotifyContentType { get; set; }
    }
}