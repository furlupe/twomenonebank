using Bank.Auth.App.Dto.Account;
using Bank.Auth.Domain.Models;
using Bank.Auth.Messages;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Bank.Auth.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IPublishEndpoint _publishEndpoint;

        public UserController(UserManager<User> userManager, IPublishEndpoint publishEndpoint)
        {
            _userManager = userManager;
            _publishEndpoint = publishEndpoint;
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

            List<Claim> claims = [
                new Claim(Claims.Subject, user.Id.ToString()),
                new Claim(Claims.Name, user.UserName),
                new Claim(ClaimTypes.Role, registerDto.Role.ToString())
            ];

            await _userManager.AddClaimsAsync(user, claims);

            await _publishEndpoint.Publish<IUserCreated>(new { user.Id });

            return Ok();
        }
    }
}
