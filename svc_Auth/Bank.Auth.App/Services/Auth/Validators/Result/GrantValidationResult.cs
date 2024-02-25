using Bank.Auth.Domain.Models;

namespace Bank.Auth.App.Services.Auth.Validators.Result
{
    public class GrantValidationResult
    {
        public bool IsSuccess { get; private set; }
        public User? User { get; private set; }
        public string? ErrorKey { get; private set; }
        public Dictionary<string, object>? AdditionalInfo { get; private set; }

        public static GrantValidationResult Success(User user) =>
            new() { IsSuccess = true, User = user };

        public static GrantValidationResult Failure(
            string errorKey,
            Dictionary<string, object>? additionalInfo = null
        ) =>
            new()
            {
                IsSuccess = false,
                ErrorKey = errorKey,
                AdditionalInfo = additionalInfo
            };
    }
}
