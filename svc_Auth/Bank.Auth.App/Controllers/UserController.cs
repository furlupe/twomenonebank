﻿using System.Security.Claims;
using Bank.Auth.App.Dto;
using Bank.Auth.Domain.Models;
using Bank.Auth.Shared.Claims;
using Bank.Auth.Shared.Enumerations;
using Bank.Auth.Shared.Extensions;
using Bank.Auth.Shared.Policies;
using Bank.Common.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Bank.Auth.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("me")]
        public async Task<ActionResult<UserDto>> GetMe()
        {
            var me = await _userManager.FindByIdAsync(User.GetId().ToString());

            if (me == null)
            {
                return BadRequest("An authorized request made by no account...what?");
            }

            _ = Enum.TryParse(me.Role, out Role role);

            return Ok(new UserDto()
            {
                Id = me.Id,
                Email = me.Email,
                Name = me.Name,
                Role = role
            });
        }

        [HttpPost("{userId}/ban")]
        public Task<IActionResult> Ban(Guid userId) => ToggleBan(userId, true);

        [HttpPost("{userId}/unban")]
        public Task<IActionResult> Unban(Guid userId) => ToggleBan(userId, false);

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (User.HasRole(Role.User))
            {
                return Forbid();
            }

            if (registerDto.Role == Role.Admin)
            {
                return BadRequest("Can't create Admin");
            }

            var user = new User() { Email = registerDto.Email, Name = registerDto.Username, UserName = registerDto.Email, Role = registerDto.Role.ToString() };
            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _userManager.AddPasswordAsync(user, registerDto.Password);

            List<Claim> claims =
            [
                new Claim(Claims.Subject, user.Id.ToString()),
                new Claim(Claims.Name, user.Email),
                new Claim(ClaimTypes.Role, registerDto.Role.ToString()),
                new Claim(BankClaims.Id, user.Id.ToString())
            ];

            await _userManager.AddClaimsAsync(user, claims);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<PageDto<UserDto>>> GetUserList([FromQuery] int page = 1)
        {
            if (User.HasRole(Role.User))
            {
                return Forbid();
            }

            return await _userManager.Users.Where(x => x.Id != User.GetId()).GetPage(new() { PageNumber = page }, user =>
            {
                _ = Enum.TryParse(user.Role, out Role role);
                return new UserDto() { Id = user.Id, Email = user.Email, Name = user.Name, Role= role };
            });
        }

        private async Task<IActionResult> ToggleBan(Guid userId, bool on = true)
        {
            if (User.HasRole(Role.User))
            {
                return Forbid();
            }

            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == userId && x.Id != User.GetId());

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
    }
}
