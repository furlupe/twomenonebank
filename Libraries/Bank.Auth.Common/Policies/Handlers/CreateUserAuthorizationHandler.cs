using Bank.Auth.Common.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace Bank.Auth.Common.Policies.Handlers
{
    public class CreateUserAuthorizationRequirement : IAuthorizationRequirement { }

    public class CreateUserAuthorizationHandler(IUserService userService)
        : AuthorizationHandler<CreateUserAuthorizationRequirement>
    {
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            CreateUserAuthorizationRequirement requirement
        )
        {
            Guid id;
            try
            {
                id = context.User.GetId();
            }
            catch (ArgumentException)
            {
                context.Fail();
                return;
            }

            if (!context.User.HasRole(Enumerations.Role.User))
            {
                context.Fail();
                return;
            }

            await userService.EnsureUserExists(id);
            context.Succeed(requirement);
        }
    }
}
