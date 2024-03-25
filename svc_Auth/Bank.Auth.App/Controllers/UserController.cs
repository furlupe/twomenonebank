using System.Security.Claims;
using Bank.Auth.App.Dto;
using Bank.Auth.Common.Attributes;
using Bank.Auth.Common.Enumerations;
using Bank.Auth.Common.Extensions;
using Bank.Auth.Domain.Models;
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
        [CalledByHuman]
        public Task<ActionResult<UserDto>> GetMe() => GetUserById(User.GetId());

        [HttpGet("{userId}")]
        [CalledByStaff]
        public Task<ActionResult<UserDto>> GetUser(Guid userId)
            => GetUserById(userId);

        [HttpGet]
        [CalledByStaff]
        public async Task<ActionResult<PageDto<UserDto>>> GetUserList([FromQuery] int page = 1)
        {
            return await _userManager
                .Users.Where(x => x.Id != User.GetId())
                .GetPage(
                    new() { PageNumber = page },
                    UserToDto
                );
        }

        [HttpPost("{userId}/ban")]
        [CalledByStaff]
        public Task<IActionResult> Ban(Guid userId) => ToggleBan(userId, true);

        [HttpPost("{userId}/unban")]
        [CalledByStaff]
        public Task<IActionResult> Unban(Guid userId) => ToggleBan(userId, false);

        [HttpPost("register")]
        [CalledByStaff]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (registerDto.Roles.Contains(Role.Admin))
            {
                return BadRequest("Can't create Admin");
            }

            foreach (var role in registerDto.Roles)
            {
                if (!Enum.IsDefined(typeof(Role), role))
                {
                    return BadRequest("Wrong role, buddy");
                }
            }

            return await CreateUser(registerDto);
        }

        private async Task<ActionResult<UserDto>> GetUserById(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return BadRequest("An authorized request made by no account...what?");
            }

            return Ok(UserToDto(user));
        }

        private static UserDto UserToDto(User user)
        {
            List<Role> roles = [];
            user.Roles.ForEach(r =>
            {
                _ = Enum.TryParse(r, out Role role);
                roles.Add(role);
            });

            return new UserDto()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Roles = roles,
                IsBanned = user.IsBanned,
                Phone = user.PhoneNumber
            };
        }

        private async Task<IActionResult> ToggleBan(Guid userId, bool on = true)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x =>
                x.Id == userId && x.Id != User.GetId() && !x.Roles.Contains(Role.Admin.ToString())
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

        private async Task<IActionResult> CreateUser(RegisterDto registerDto)
        {
            if (await _userManager.Users.AnyAsync(u => u.PhoneNumber == registerDto.Phone))
            {
                return BadRequest("phone_taken");
            }

            List<string> stringRoles = registerDto.Roles.Select(r => r.ToString()).ToList();

            var user = new User()
            {
                Email = registerDto.Email,
                Name = registerDto.Username,
                UserName = registerDto.Email,
                Roles = stringRoles
            };

            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded) return BadRequest(result.Errors);

            result = await _userManager.AddPasswordAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            result = await _userManager.SetPhoneNumberAsync(user, registerDto.Phone);
            if (!result.Succeeded) return BadRequest(result.Errors);

            List<Claim> claims =
            [
                new Claim(Claims.Subject, user.Id.ToString()),
            ];
            stringRoles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));

            await _userManager.AddClaimsAsync(user, claims);

            return Ok();
        }
    }
}
