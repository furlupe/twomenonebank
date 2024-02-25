using Bank.Auth.Domain.Models;

namespace Bank.Auth.App.Services.Auth.Validators.Result
{
    public static class GrantValidationResultErrorKeys
    {
        public const string InvalidUsernameOrPassword = "invalid_username_or_password";
    }

    public class GrantValidationResultFactory
    {
        public GrantValidationResult Success(User user) => GrantValidationResult.Success(user);

        public GrantValidationResult InvalidCredentials() =>
            GrantValidationResult.Failure(GrantValidationResultErrorKeys.InvalidUsernameOrPassword);
    }
}
