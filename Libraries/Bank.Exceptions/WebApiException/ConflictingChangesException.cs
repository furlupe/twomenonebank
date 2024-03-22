using Microsoft.AspNetCore.Http;

namespace Bank.Exceptions.WebApiException;

public class ConflictingChangesException : ProblemDetailsException
{
    public ConflictingChangesException(string? detail = null)
        : base(
            ErrorTypes.ConflictingChanges,
            StatusCodes.Status409Conflict,
            "Operation resulted in a conflict with the current state of the resource.",
            detail
        ) { }
}
