using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        [HttpGet]
        public IActionResult NotificationLİst()
        {
            return Ok(_notificationService.TGetListAll());
        }
        [HttpGet("NotificationCountbyStatusFalse")]
        public IActionResult NotificationCountbyStatusFalse()
        {
            return Ok(_notificationService.TNotificationCountbyStatusFalse());
        }
        [HttpGet("GetAllNotificationByFalse")]
        public IActionResult GetAllNotificationByFalse()
        {
            return Ok(_notificationService.TGetAllNotificationByFalse());
        }

    }
}
