using System.Security.Claims;
using Bank.Auth.App.Services.Auth.Validators;
using Bank.Auth.App.Services.Auth.Validators.Result;
using Bank.Auth.Domain.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Bank.Auth.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly GrantValidatorFactory _grantValidatorFactory;
        private readonly SignInManager<User> _signInManager;

        public AuthenticationController(
            GrantValidatorFactory grantValidatorFactory,
            SignInManager<User> signInManager,
            UserManager<User> userManager
        )
        {
            _grantValidatorFactory = grantValidatorFactory;
            _signInManager = signInManager;
        }

        [HttpPost("~/connect/token")]
        public async Task<IActionResult> Exchange()
        {
            var request = HttpContext.GetOpenIddictServerRequest();
            ArgumentNullException.ThrowIfNull(request);

            BaseGrantValidator grantValidator = _grantValidatorFactory.Create(request);
            var validationResult = await grantValidator.ValidateAsync(request);

            return await ProccessGrantValidationResult(validationResult, request);
        }

        private async Task<IActionResult> ProccessGrantValidationResult(
            GrantValidationResult result,
            OpenIddictRequest request
        )
        {
            if (!result.IsSuccess || result.User == null)
            {
                return Unauthorized(new { error = result.ErrorKey, info = result.AdditionalInfo });
            }

            var principal = await _signInManager.CreateUserPrincipalAsync(result.User);

            principal.SetDestinations(claim =>
                claim.Type switch
                {
                    Claims.Name
                    or Claims.Subject
                        => [Destinations.AccessToken, Destinations.IdentityToken],
                    _ => [Destinations.AccessToken]
                }
            );
            principal.SetScopes(request.GetScopes());

            var signInResult = SignIn(
                principal,
                OpenIddictServerAspNetCoreDefaults.AuthenticationScheme
            );
            return signInResult;
        }
    }
}
