using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EgitimAPI.ApplicationCore.Services.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.NotificationService.Dto;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.NotificationService
{
    public interface INotificationAppService
    {
        Task CreateNotify(CreateNotificationDto input);
        Task<PagedResultDto<NotificationDto>> GetEntityNotifications(long ownerId,string ownerType);
    }
}