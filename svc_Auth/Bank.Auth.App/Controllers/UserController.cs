using System.Security.Claims;
using Bank.Auth.App.Dto.Account;
using Bank.Auth.Domain.Models;
using Bank.Auth.Shared.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Bank.Auth.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user = new User() { Email = registerDto.Email, UserName = registerDto.Email };
            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _userManager.AddPasswordAsync(user, registerDto.Password);

            List<Claim> claims =
            [
                new Claim(Claims.Subject, user.Id.ToString()),
                new Claim(Claims.Name, user.UserName),
                new Claim(ClaimTypes.Role, registerDto.Role.ToString()),
                new Claim(BankClaims.Id, user.Id.ToString())
            ];

            await _userManager.AddClaimsAsync(user, claims);

            return Ok();
        }
    }
}
