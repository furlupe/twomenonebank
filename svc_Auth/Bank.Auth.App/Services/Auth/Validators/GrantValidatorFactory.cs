using Bank.Auth.App.Exceptions;
using Bank.Auth.App.Services.Auth.Validators.Result;
using Bank.Auth.Domain.Models;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;

namespace Bank.Auth.App.Services.Auth.Validators
{
    public class GrantValidatorFactory
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _httpAccessor;
        private readonly GrantValidationResultFactory _grantValidationResultFactory;

        public GrantValidatorFactory(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IHttpContextAccessor httpAccessor,
            GrantValidationResultFactory grantValidationResultFactory
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpAccessor = httpAccessor;
            _grantValidationResultFactory = grantValidationResultFactory;
        }

        public BaseGrantValidator Create(OpenIddictRequest request)
        {
            if (request.IsPasswordGrantType())
            {
                return new PasswordGrantValidator(_userManager, _grantValidationResultFactory);
            }

            if (request.IsRefreshTokenGrantType())
            {
                return new RefreshTokenGrantValidator(
                    _userManager,
                    _signInManager,
                    _httpAccessor,
                    _grantValidationResultFactory
                );
            }

            throw new UnsupportedGrantValidator();
        }
    }
}
