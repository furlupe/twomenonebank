using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Exceptions.WebApiException;

public class FailedRequestException : ProblemDetailsException
{
    private const string _msg = "An error occured while communicating to an external service.";

    public FailedRequestException(string message)
        : base(
            ErrorTypes.InternalServerError,
            StatusCodes.Status500InternalServerError,
            _msg,
            message
        ) { }

    public FailedRequestException(HttpResponseMessage response)
        : this(GetDetail(response)) { }

    public FailedRequestException(ProblemDetails original, HttpResponseMessage response)
        : this(GetDetail(original, response)) { }

    private static string GetDetail(ProblemDetails original, HttpResponseMessage response)
    {
        return GetDetail(response) + " " + $"{original.Title} {original.Detail}";
    }

    private static string GetDetail(HttpResponseMessage response)
    {
        var request = response.RequestMessage;

        return (request is null ? "Request" : $"{request.Method} request")
            + $" failed to "
            + (request is null ? "an external service" : $"'{request.RequestUri}'")
            + $" with status code {(int)response.StatusCode} ({response.ReasonPhrase}).";
    }
}
