using Bank.Auth.Common.Claims;
using Bank.Auth.Common.Enumerations;
using Microsoft.AspNetCore.Authentication;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Bank.Auth.App.AuthenticationValidators.Validators
{
    public class AuthorizationCodeGrantValidator : BaseGrantValidator
    {
        private IHttpContextAccessor _httpContext;
        public AuthorizationCodeGrantValidator(IHttpContextAccessor httpContext) 
        {
            _httpContext = httpContext;
        }
        protected async override Task<GrantValidationResult> CommitValidation(OpenIddictRequest request)
        {
            if (_httpContext.HttpContext == null)
            {
                return GrantValidationResult.Failure("httpcontext_missing");
            }

            var claimsPrincipal = (
                await _httpContext.HttpContext.AuthenticateAsync(
                    OpenIddictServerAspNetCoreDefaults.AuthenticationScheme
                )
            ).Principal;

            if (claimsPrincipal == null)
                return GrantValidationResult.Failure("principal_empty");

            claimsPrincipal.AddClaim(BankClaims.Caller, Caller.Human.ToString());
            claimsPrincipal.SetDestinations(_ =>
                [Destinations.AccessToken, Destinations.IdentityToken]
            );

            return GrantValidationResult.Success(claimsPrincipal);
        }
    }
}
