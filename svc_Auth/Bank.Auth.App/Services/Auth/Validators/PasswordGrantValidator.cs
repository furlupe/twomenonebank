using Bank.Auth.App.Services.Auth.Validators.Result;
using Bank.Auth.Domain.Models;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;

namespace Bank.Auth.App.Services.Auth.Validators
{
    public class PasswordGrantValidator : BaseGrantValidator
    {
        private readonly UserManager<User> _userManager;

        public PasswordGrantValidator(
            UserManager<User> userManager,
            GrantValidationResultFactory grantValidationResultFactory
        )
            : base(grantValidationResultFactory)
        {
            _userManager = userManager;
        }

        protected override async Task<GrantValidationResult> CommitValidation(
            OpenIddictRequest request
        )
        {
            (string username, string password) = ExtractDataFromRequest(request);

            var user = await _userManager.FindByEmailAsync(username);
            if (user == null)
            {
                return _grantResultFactory.InvalidCredentials();
            }

            bool isPasswordRight = await _userManager.CheckPasswordAsync(user, password);
            if (!isPasswordRight)
            {
                return _grantResultFactory.InvalidCredentials();
            }

            return _grantResultFactory.Success(user);
        }

        private static (string, string) ExtractDataFromRequest(OpenIddictRequest request)
        {
            ArgumentNullException.ThrowIfNull(request.Username);
            ArgumentNullException.ThrowIfNull(request.Password);

            return (request.Username, request.Password);
        }
    }
}
