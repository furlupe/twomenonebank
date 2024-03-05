using Bank.Auth.Shared.Claims;
using Bank.Auth.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Bank.Auth.Shared.Policies.Handlers
{
    public class CreateUserAuthorizationRequirement : IAuthorizationRequirement { }
    public class CreateUserAuthorizationHandler : AuthorizationHandler<CreateUserAuthorizationRequirement>
    {
        private readonly IUserService _userService;
        public CreateUserAuthorizationHandler(IUserService userService)
        {
            _userService = userService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreateUserAuthorizationRequirement requirement)
        {
            var id = context.User.GetIdOrDefault();

            if (id == null)
            {
                context.Fail();
                return;
            }

            if (!await _userService.Exists((Guid) id))
            {
                await _userService.Create((Guid) id);
            }

            context.Succeed(requirement);
            return;
        }
    }
}
