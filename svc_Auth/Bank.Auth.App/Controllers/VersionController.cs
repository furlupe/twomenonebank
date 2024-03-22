using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;

namespace Bank.Auth.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : Controller
    {
        [HttpGet]
        public string Version() =>
            typeof(VersionController)
                .Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                ?.InformationalVersion ?? "";

        [HttpGet("authenticated")]
        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        public string VersionAuthenticated() => Version();

        [HttpGet("view")]
        public IActionResult Index() => View((object) Version());
    }
}
