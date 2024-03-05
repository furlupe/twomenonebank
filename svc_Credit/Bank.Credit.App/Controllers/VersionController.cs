using System.Reflection;
using Bank.Auth.Shared.Policies;
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
        public string VersionUser() => Version();

        [HttpGet("authenticated/not-user")]
        [Authorize(Policy = Policies.EmployeeOrHigher)]
        public string VersionNotForUsers() => Version();
    }
}
