using System.Security.Claims;
using Bank.Auth.Common.Claims;
using Bank.Auth.Common.Enumerations;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Bank.Auth.App.AuthenticationValidators.Validators
{
    public class ClientCredentialsGrantValidator : BaseGrantValidator
    {
        protected override Task<GrantValidationResult> CommitValidation(OpenIddictRequest request)
        {
            if (request.ClientId == null)
            {
                return Task.FromResult(GrantValidationResult.Failure("client_id_missing"));
            }

            var identity = new ClaimsIdentity(
                OpenIddictServerAspNetCoreDefaults.AuthenticationScheme
            );
            identity.AddClaim(Claims.Subject, request.ClientId);
            identity.AddClaim(BankClaims.Caller, Caller.Service.ToString());

            ClaimsPrincipal claimsPrincipal = new(identity);
            claimsPrincipal.SetDestinations(claim => [Destinations.AccessToken]);
            claimsPrincipal.SetScopes(request.GetScopes());

            return Task.FromResult(GrantValidationResult.Success(claimsPrincipal));
        }
    }
}
