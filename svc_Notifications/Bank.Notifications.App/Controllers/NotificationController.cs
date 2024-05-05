using Bank.Auth.Common.Attributes;
using Bank.Notifications.App.Services;
using Bank.Notifications.Persistence;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bank.Notifications.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize, CalledByService]
    public class NotificationController : ControllerBase
    {
        private readonly BankNotificationsDbContext _dbContext;
        private readonly BankNotificationService _notificationService;

        public NotificationController(
            BankNotificationsDbContext dbContext,
            BankNotificationService notificationService
        )
        {
            _dbContext = dbContext;
            _notificationService = notificationService;
        }

        [HttpPost("employee")]
        public Task NotifyEmployees([FromBody] IEnumerable<Notification> messages)
            => _notificationService.NotifyEmployees(messages);

        [HttpPost("customer/{customerId}")]
        public async Task NotifyCustomer([FromRoute] Guid customerId, [FromBody] IEnumerable<Notification> messages)
        {
            var user = await _dbContext.Users.SingleAsync(u => u.Id == customerId);

            await _notificationService.NotifyCustomer(user.Token, messages);
        }
    }
}
