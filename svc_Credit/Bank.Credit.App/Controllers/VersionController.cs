using System.Reflection;
using Bank.Auth.Common.Attributes;
using Bank.Auth.Common.Enumerations;
using Bank.Auth.Common.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Credit.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        [HttpGet]
        public string Version() =>
            typeof(VersionController)
                .Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                ?.InformationalVersion ?? "";

        [Authorize, HttpGet("authenticated")]
        [Authorize]
        public string VersionAuthenticated() => Version();

        [HttpGet("authenticated/user")]
        [Authorize(Policy = Policies.CreateUserIfNeeded)]
        [CalledByUser]
        public string VersionUser() => Version();

        [HttpGet("authenticated/not-user")]
        [Authorize]
        [CalledByStaff]
        public string VersionNotForUsers() => Version();
    }
}
