using Bank.Auth.Common.Attributes;
using Bank.Auth.Common.Extensions;
using Bank.Auth.Common.Policies;
using Bank.Credit.App.Dto;
using Bank.Credit.App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Credit.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly CreditUserService _userService;

        public UserController(CreditUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("me"), CalledByUser, Authorize(Policy = Policies.CreateUserIfNeeded)]
        public Task<UserDto> GetMe() => _userService.GetUser(User.GetId());

        [HttpGet("{userId}"), CalledByStaff]
        public Task<UserDto> GetUser(Guid userId) => _userService.GetUser(userId);
    }
}
