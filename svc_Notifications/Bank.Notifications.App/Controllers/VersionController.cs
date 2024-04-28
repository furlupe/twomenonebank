using Bank.Notifications.Http.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Bank.Notifications.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        private readonly NotificationsClient _client;

        public VersionController(NotificationsClient client)
        {
            _client = client;
        }

        [HttpGet]
        public string Version() =>
            typeof(VersionController)
                .Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                ?.InformationalVersion ?? "";

        [HttpGet("authenticated")]
        [Authorize]
        public string VersionAuthorized() => Version();
    }
}
