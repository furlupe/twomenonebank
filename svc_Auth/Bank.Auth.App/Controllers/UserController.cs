using System.Security.Claims;
using Bank.Auth.App.Dto.Account;
using Bank.Auth.Domain.Models;
using Bank.Auth.Shared.Claims;
using Bank.Auth.Shared.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Bank.Auth.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = Policies.EmployeeOrHigher)]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("{userId}/ban")]
        public Task<IActionResult> Ban(Guid userId) => ToggleBan(userId, true);

        [HttpPost("{userId}/unban")]
        public Task<IActionResult> Unban(Guid userId) => ToggleBan(userId, false);

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (registerDto.Role == Shared.Enumerations.Role.Admin)
            {
                return BadRequest("Can't create Admin");
            }

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

        private async Task<IActionResult> ToggleBan(Guid userId, bool on = true)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                return BadRequest($"Couldn't find user w/ id = {userId}");
            }

            var result = await _userManager.SetLockoutEndDateAsync(
                user,
                on ? DateTime.MaxValue : null
            );
            return result.Succeeded ? Ok() : BadRequest();
        }
    }
}
