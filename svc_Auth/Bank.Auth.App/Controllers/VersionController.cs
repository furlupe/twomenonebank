using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Auth.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public string Version() =>
            typeof(VersionController)
                .Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                ?.InformationalVersion ?? "";

        [HttpGet("authenticated")]
        public string VersionAuthenticated() => Version();
    }
}
