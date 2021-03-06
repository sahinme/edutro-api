using Microsoft.EgitimAPI.ApplicationCore.Entities;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Notifications;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.NotificationService.Dto
{
    public class CreateNotificationDto
    {
        public string Title { get; set; }
        public long SenderId { get; set; }
        public EntityType SenderType { get; set; }
        public long ContentId { get; set; }
        public string Content { get; set; }
        public NotifyContentType NotifyContentType { get; set; }
        public long OwnerId { get; set; }
        public EntityType OwnerType { get; set; }
        public bool IsRead { get; set; }
    }
}