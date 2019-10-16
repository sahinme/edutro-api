using Microsoft.EgitimAPI.ApplicationCore.Entities.Courses;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Educators;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Users;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;

namespace Microsoft.EgitimAPI.ApplicationCore.Entities.Notifications
{
    public class Notification:BaseEntity,IAggregateRoot
    {
        public string Content { get; set; }
        public string Title { get; set; }
        public long SenderId { get; set; }
        public string SenderType { get; set; }
        public long OwnerId { get; set; }
        public string OwnerType { get; set; }
        public bool IsRead { get; set; }
    }
}