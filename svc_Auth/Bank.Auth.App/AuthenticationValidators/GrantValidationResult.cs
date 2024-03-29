using System.Security.Claims;

namespace Bank.Auth.App.AuthenticationValidators
{
    public class GrantValidationResult
    {
        public ClaimsPrincipal? User { get; private set; } = null;
        public bool IsSuccess { get; private set; }
        public string? ErrorMessage { get; private set; } = null;

        public static GrantValidationResult Success(ClaimsPrincipal user) =>
            new() { User = user, IsSuccess = true };

        public static GrantValidationResult Failure(string? message = null) =>
            new() { IsSuccess = false, ErrorMessage = message };
    }
}
