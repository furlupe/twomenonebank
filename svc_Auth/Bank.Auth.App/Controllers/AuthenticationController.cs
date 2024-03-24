using System.Security.Claims;
using Bank.Auth.App.ViewModels;
using Bank.Auth.Common.Claims;
using Bank.Auth.Common.Enumerations;
using Bank.Auth.Domain.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Bank.Auth.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        public readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthenticationController(
            UserManager<User> userManager,
            SignInManager<User> signInManager
        )
        {
            _signInManager = signInManager;
            _userManager = userManager;
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

            ClaimsPrincipal? claimsPrincipal = null;

            if (request.IsPasswordGrantType())
            {
                var user = await FindUserByCredentials(request.Username!, request.Password!);
                if (user == null)
                    throw new InvalidOperationException("invalid_username_password");

                var claims = await _userManager.GetClaimsAsync(user);

                var identity = new ClaimsIdentity(
                    claims,
                    OpenIddictServerAspNetCoreDefaults.AuthenticationScheme
                );

                claimsPrincipal = new ClaimsPrincipal(identity);
                claimsPrincipal.SetDestinations(claim =>
                    [Destinations.AccessToken, Destinations.IdentityToken]
                );
                claimsPrincipal.SetScopes(request.GetScopes());
            }
            else if (request.IsAuthorizationCodeGrantType() || request.IsRefreshTokenGrantType())
            {
                claimsPrincipal = (
                    await HttpContext.AuthenticateAsync(
                        OpenIddictServerAspNetCoreDefaults.AuthenticationScheme
                    )
                ).Principal;

                if (claimsPrincipal == null)
                    throw new InvalidOperationException("ClaimsPrincipal is null");

                claimsPrincipal.AddClaim(BankClaims.Caller, Caller.Human.ToString());
                claimsPrincipal.SetDestinations(_ =>
                    [Destinations.AccessToken, Destinations.IdentityToken]
                );
            }
            else
            {
                throw new InvalidOperationException("Unsupported grant");
            }

            if (claimsPrincipal == null)
                throw new InvalidOperationException("ClaimsPrincipal is null");

            return SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        [HttpGet("~/connect/authorize")]
        [HttpPost("~/connect/authorize")]
        public async Task<IActionResult> Authorize()
        {
            var request = HttpContext.GetOpenIddictServerRequest();
            ArgumentNullException.ThrowIfNull(request);

            var result = await HttpContext.AuthenticateAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            if (!result.Succeeded)
            {
                var properties = new AuthenticationProperties()
                {
                    RedirectUri =
                        Request.PathBase
                        + Request.Path
                        + QueryString.Create(
                            Request.HasFormContentType ? [.. Request.Form] : [.. Request.Query]
                        ),
                };

                return Challenge(
                    authenticationSchemes: CookieAuthenticationDefaults.AuthenticationScheme,
                    properties: properties
                );
            }

            var claims = result.Principal.Claims;

            foreach (Claim claim in claims)
            {
                claim.SetDestinations(Destinations.AccessToken);
            }

            var claimsIdentity = new ClaimsIdentity(
                claims,
                OpenIddictServerAspNetCoreDefaults.AuthenticationScheme
            );

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            claimsPrincipal.SetScopes(request.GetScopes());

            return SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        [HttpGet("~/login")]
        public IActionResult Login([FromQuery] string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost("~/login")]
        public async Task<IActionResult> Login([FromForm] LoginViewModel model)
        {
            ViewBag.ReturnUrl = model.ReturnUrl;

            User? user = await FindUserByCredentials(model.Email, model.Password);
            if (user == null)
            {
                return View(model);
            }

            var principal = await _signInManager.CreateUserPrincipalAsync(user);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal
            );

            if (Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }

            return Ok();
        }

        private async Task<User?> FindUserByCredentials(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return null;

            bool isPasswordRight = await _userManager.CheckPasswordAsync(user, password);
            if (!isPasswordRight)
                return null;

            return user;
        }
    }
}
