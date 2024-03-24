using Microsoft.AspNetCore.Http;

namespace Bank.Exceptions.WebApiException;

public class ImATeapotException : ProblemDetailsException
{
    public ImATeapotException(string? message = null)
        : base(
            ErrorTypes.InternalServerError,
            StatusCodes.Status418ImATeapot,
            "Request has failed due to the server being a teapot.",
            message
        ) { }
}
