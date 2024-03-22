using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Exceptions.WebApiException;

public interface IWebApiException
{
    IActionResult ToResult();
}

public interface IWebApiException<out TDetails> : IWebApiException
    where TDetails : ProblemDetails
{
    public TDetails Details { get; }
}

public interface IWebApiContentException : IWebApiException
{
    string Content { get; }
    public string? ContentType { get; }
    public HttpStatusCode StatusCode { get; }
}
