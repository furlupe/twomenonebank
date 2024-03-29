using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Exceptions.WebApiException;

public class FailedRequestException : ProblemDetailsException
{
    private const string _msg = "An error occured while communicating to a service.";

    public FailedRequestException(string message)
        : base(
            ErrorTypes.InternalServerError,
            StatusCodes.Status500InternalServerError,
            _msg,
            message
        ) { }

    public FailedRequestException(ProblemDetails original, string? message = null)
        : base(
            original.Type ?? ErrorTypes.InternalServerError,
            original.Status ?? StatusCodes.Status500InternalServerError,
            original.Title,
            message ?? original.Detail
        ) { }

    public FailedRequestException(ProblemDetails original, HttpResponseMessage response)
        : this(original, GetDetail(original, response)) { }

    private static string GetDetail(ProblemDetails original, HttpResponseMessage response)
    {
        return GetDetail(response) + " " + $"{original.Title} {original.Detail}";
    }

    public FailedRequestException(HttpResponseMessage response)
        : this(GetDetail(response)) { }

    private static string GetDetail(ProblemDetails original)
    {
        return $"Request failed with status code {original.Status}: {original.Title} {original.Detail}";
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
