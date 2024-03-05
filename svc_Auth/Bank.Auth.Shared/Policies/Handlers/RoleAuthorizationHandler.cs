using Bank.Auth.Shared.Enumerations;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Bank.Auth.Shared.Policies.Handlers
{
    public class RoleAuthorizationRequirement : IAuthorizationRequirement
    {
        public IEnumerable<Role> Roles { get; }

        public RoleAuthorizationRequirement(IEnumerable<Role> roles)
        {
            Roles = roles;
        }
    }
    public class RoleAuthorizationHandler : AuthorizationHandler<RoleAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleAuthorizationRequirement requirement)
        {
            var roleClaim = context.User.FindFirst(x => x.Type == ClaimTypes.Role);
            if (roleClaim == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var parseResult = Enum.TryParse(roleClaim.Value, out Role role);
            if (parseResult == false || !requirement.Roles.Contains(role))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
