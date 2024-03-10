using Admonish;

namespace Bank.Common.Utils;

public static class Validation
{
    public static ValidationResult Check(string key, bool condition, string message) =>
        Validator.Create().Check(key, condition, message).ThrowIfInvalid();
}
