using Bank.Auth.Common.Attributes;
using Bank.Auth.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Validation.AspNetCore;

namespace Bank.Auth.App.Controllers
{
    [Route("api/User/")]
    [ApiController]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public class InternalServicesController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public InternalServicesController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("id"), CalledByService]
        public async Task<Guid> GetUserIdByPhone([FromQuery] string phoneNumber)
            => (await _userManager.Users.SingleAsync(x => x.PhoneNumber == phoneNumber)).Id;
    }
}
