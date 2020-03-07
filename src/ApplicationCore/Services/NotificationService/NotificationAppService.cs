using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EgitimAPI.ApplicationCore.Entities;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Notifications;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;
using Microsoft.EgitimAPI.ApplicationCore.Services.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.NotificationService.Dto;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.NotificationService
{
    public class NotificationAppService:INotificationAppService
    {
        private readonly IAsyncRepository<Notification> _notificationRepository;

        public NotificationAppService(IAsyncRepository<Notification> notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public async Task CreateNotify(CreateNotificationDto input)
        {
            var notify = new Notification
            {
                OwnerId = input.OwnerId,
                SenderId = input.SenderId,
                OwnerType = input.OwnerType,
                SenderType = input.SenderType,
                Title = input.Title,
                ContentId = input.ContentId,
                Content = input.Content,
                NotifyContentType = input.NotifyContentType
            };
            await _notificationRepository.AddAsync(notify);
        }

        public async Task<PagedResultDto<NotificationDto>> GetEntityNotifications(long ownerId, EntityType ownerType)
        {
            var notifies = await _notificationRepository.GetAll().Where(x => x.OwnerId == ownerId && x.OwnerType == ownerType)
                .Select(x => new NotificationDto
                {
                    Title = x.Title,
                    IsRead = x.IsRead,
                    Content = x.Content,
                    ContentId = x.ContentId,
                    NotifyContentType = x.NotifyContentType
                }).ToListAsync();
           
            return new PagedResultDto<NotificationDto>
            {
                Results = notifies,
                Count = notifies.Count
            };
            
        }
    }
}