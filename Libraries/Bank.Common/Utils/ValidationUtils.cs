using Admonish;

namespace Bank.Common.Utils;

public static class ValidationUtils
{
    public static ValidationResult Check(string key, bool condition, string message) =>
        Validator.Create().Check(key, condition, message).ThrowIfInvalid();
}
