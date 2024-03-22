using System.Security.Claims;
using Bank.Auth.App.Dto;
using Bank.Auth.Domain.Models;
using Bank.Auth.Shared.Claims;
using Bank.Auth.Shared.Enumerations;
using Bank.Auth.Shared.Extensions;
using Bank.Common.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Validation.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Bank.Auth.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("me")]
        public Task<ActionResult<UserDto>> GetMe()
            => GetUserById(User.GetId());

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDto>> GetUser(Guid userId)
        {
            if (!IsEmployeeOrHigher())
            {
                return Forbid();
            }

            return await GetUserById(userId);
        }

        [HttpGet]
        public async Task<ActionResult<PageDto<UserDto>>> GetUserList([FromQuery] int page = 1)
        {
            if (!IsEmployeeOrHigher())
            {
                return Forbid();
            }

            return await _userManager
                .Users.Where(x => x.Id != User.GetId())
                .GetPage(
                    new() { PageNumber = page },
                    user =>
                    {
                        _ = Enum.TryParse(user.Role, out Role role);
                        return new UserDto()
                        {
                            Id = user.Id,
                            Email = user.Email,
                            Name = user.Name,
                            Role = role,
                            IsBanned = user.LockoutEnd != null
                        };
                    }
                );
        }

        [HttpPost("{userId}/ban")]
        public Task<IActionResult> Ban(Guid userId) => ToggleBan(userId, true);

        [HttpPost("{userId}/unban")]
        public Task<IActionResult> Unban(Guid userId) => ToggleBan(userId, false);

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!IsEmployeeOrHigher())
            {
                return Forbid();
            }

            if (registerDto.Role == Role.Admin)
            {
                return BadRequest("Can't create Admin");
            }

            if (!Enum.IsDefined(typeof(Role), registerDto.Role))
            {
                return BadRequest("Wrong role, buddy");
            }

            var user = new User()
            {
                Email = registerDto.Email,
                Name = registerDto.Username,
                UserName = registerDto.Email,
                Role = registerDto.Role.ToString()
            };
            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _userManager.AddPasswordAsync(user, registerDto.Password);

            List<Claim> claims =
            [
                new Claim(Claims.Subject, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Role, registerDto.Role.ToString()),
            ];

            await _userManager.AddClaimsAsync(user, claims);

            return Ok();
        }

        private async Task<ActionResult<UserDto>> GetUserById(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return BadRequest("An authorized request made by no account...what?");
            }

            _ = Enum.TryParse(user.Role, out Role role);

            return Ok(
                new UserDto()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    Role = role,
                    IsBanned = user.LockoutEnd != null
                }
            );
        }

        private async Task<IActionResult> ToggleBan(Guid userId, bool on = true)
        {
            if (!IsEmployeeOrHigher())
            {
                return Forbid();
            }

            var user = await _userManager.Users.SingleOrDefaultAsync(x =>
                x.Id == userId && x.Id != User.GetId() && x.Role != Role.Admin.ToString()
            );

            if (user == null)
            {
                return BadRequest($"Couldn't find user w/ id = {userId}");
            }

            var result = await _userManager.SetLockoutEndDateAsync(
                user,
                on ? DateTime.SpecifyKind(DateTime.MaxValue, DateTimeKind.Utc) : null
            );
            return result.Succeeded ? Ok() : BadRequest();
        }
        private bool IsEmployeeOrHigher()
            => User.HasRole(Role.Employee) || User.HasRole(Role.Admin);
    }
}
