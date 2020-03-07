using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EgitimAPI.ApplicationCore.Entities;
using Microsoft.EgitimAPI.ApplicationCore.Services.NotificationService;
using Microsoft.EgitimAPI.ApplicationCore.Services.NotificationService.Dto;

namespace Microsoft.EgitimAPI.Web.Controllers.Api
{
    public class NotificationController:BaseApiController
    {
        private readonly INotificationAppService _notificationAppService;

        public NotificationController(INotificationAppService notificationAppService)
        {
            _notificationAppService = notificationAppService;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateNotify(CreateNotificationDto input)
        {
            await _notificationAppService.CreateNotify(input);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetNotifies(long ownerId, EntityType ownerType)
        {
            var result = await _notificationAppService.GetEntityNotifications(ownerId, ownerType);
            return Ok(result);
        }
    }
}