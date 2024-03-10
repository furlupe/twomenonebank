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

        [HttpGet]
        public async Task<ActionResult<PageDto<UserDto>>> GetUserList([FromQuery] int page = 1)
        {
            if (User.HasRole(Role.User))
            {
                return Forbid();
            }

            return await _userManager.Users.GetPage(new() { PageNumber = page }, user => new UserDto() { Id = user.Id, Email = user.Email });
        }

        private async Task<IActionResult> ToggleBan(Guid userId, bool on = true)
        {
            if (User.HasRole(Role.User))
            {
                return Forbid();
            }

            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == userId);

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
