using Bank.Auth.App.Services.Auth.Validators;
using Bank.Auth.App.Services.Auth.Validators.Result;
using Bank.Auth.Domain.Models;
using Microsoft.AspNetCore;
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
            SignInManager<User> signInManager
        )
        {
            _grantValidatorFactory = grantValidatorFactory;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Produces token
        /// </summary>
        /// <remarks>
        /// In order to get access and refresh tokens, one must provide following parameters in form of 'x-www-form-urlencoded'
        /// <br/>
        /// **grant_type**: how client want's to get tokens (possible value: 'password')
        /// <br/>
        /// **scope**: what data does one need (value: 'offline_access')
        /// <br/>
        /// **client_id**: ID of the client, requesting tokens (value: 'amogus')
        /// <br/>
        /// <br/>
        /// Depending on 'grant_type', some parameters change.
        /// <br/>
        /// E.g., using password grant_type requires one to provide 'username' and 'password'\n
        /// <br/>
        /// p.s.: no fxking idea how to enable if method uses openIddict's request thing - impossible, ig
        /// </remarks>

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
