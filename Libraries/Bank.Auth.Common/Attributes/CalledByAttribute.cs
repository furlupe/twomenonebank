using System.Security.Claims;
using Bank.Auth.Common.Claims;
using Bank.Auth.Common.Enumerations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace Bank.Auth.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CalledByAttribute : Attribute, IAuthorizationFilter
    {
        readonly Claim _callerClaim;
        readonly List<Claim> _roleClaims = [];

        public CalledByAttribute(Caller caller)
        {
            _callerClaim = new Claim(BankClaims.Caller, caller.ToString());
        }

        public CalledByAttribute(Caller caller, params Role[] roles)
            : this(caller)
        {
            foreach (var role in roles)
            {
                _roleClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool hasClaims = context.HttpContext.User.HasClaim(c =>
                c.Type == _callerClaim.Type && c.Value == _callerClaim.Value
            );

            hasClaims &= _roleClaims.IsNullOrEmpty() || _roleClaims.Any(role =>
                context.HttpContext.User.HasClaim(c => c.Type == role.Type && c.Value == role.Value)
            );

            if (!hasClaims)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            }
        }
    }
}
