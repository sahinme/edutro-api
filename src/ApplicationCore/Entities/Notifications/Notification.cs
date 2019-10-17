using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;
using Newtonsoft.Json;

namespace Microsoft.EgitimAPI.ApplicationCore.Entities.Notifications
{
    public class Notification:BaseEntity,IAggregateRoot
    {
        public string Content { get; set; }
        public long ContentId { get; set; }
        public string Title { get; set; }
        public long SenderId { get; set; }
        public string SenderType { get; set; }
        public long OwnerId { get; set; }
        public string OwnerType { get; set; }
        public NotifyContentType NotifyContentType { get; set; }
        
        [DefaultValue(false)]
        public bool IsRead { get; set; }
    }

}