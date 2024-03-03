using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Exceptions;

public class ValidationException : WebApiException<ValidationProblemDetails>
{
    public ValidationException(string message)
        : base(ErrorTypes.ValidationError, StatusCodes.Status400BadRequest, null, message) { }

    public ValidationException(IDictionary<string, string[]> errors)
        : this(ErrorsToString(errors))
    {
        Details.Errors = errors;
    }

    private static string ErrorsToString(IDictionary<string, string[]> errors)
    {
        IEnumerable<string> errs = from kv in errors from v in kv.Value select $"{kv.Key}: {v}";
        return string.Join("\n", errs);
    }
}
