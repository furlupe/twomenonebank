using Bank.Auth.App.Services.Auth.Validators.Result;
using Bank.Auth.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace Bank.Auth.App.Services.Auth.Validators
{
    public class RefreshTokenGrantValidator : BaseGrantValidator
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RefreshTokenGrantValidator(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IHttpContextAccessor httpContextAccessor,
            GrantValidationResultFactory grantValidationResultFactory
        )
            : base(grantValidationResultFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<GrantValidationResult> CommitValidation(
            OpenIddictRequest request
        )
        {
            if (_httpContextAccessor.HttpContext == null)
            {
                return _grantResultFactory.InvalidCredentials();
            }

            var info = await _httpContextAccessor.HttpContext.AuthenticateAsync(
                OpenIddictServerAspNetCoreDefaults.AuthenticationScheme
            );
            if (info == null || info.Principal == null)
            {
                return _grantResultFactory.InvalidCredentials();
            }

            var user = await _userManager.GetUserAsync(info.Principal);
            if (user == null || !await _signInManager.CanSignInAsync(user))
            {
                return _grantResultFactory.InvalidCredentials();
            }

            return _grantResultFactory.Success(user);
        }
    }
}
