using Bank.Auth.Domain.Models;

namespace Bank.Auth.App.Services.Auth.Validators.Result
{
    public static class GrantValidationResultErrorKeys
    {
        public const string InvalidUsernameOrPassword = "invalid_username_or_password";
        public const string Banned = "user_banned";
    }

    public class GrantValidationResultFactory
    {
        public GrantValidationResult Success(User user) => GrantValidationResult.Success(user);

        public GrantValidationResult InvalidCredentials() =>
            GrantValidationResult.Failure(GrantValidationResultErrorKeys.InvalidUsernameOrPassword);

        public GrantValidationResult Banned() =>
            GrantValidationResult.Failure(GrantValidationResultErrorKeys.Banned);
    }
}
