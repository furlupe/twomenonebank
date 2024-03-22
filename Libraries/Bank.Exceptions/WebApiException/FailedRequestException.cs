using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Exceptions.WebApiException;

public class FailedRequestException : ApplicationException, IWebApiContentException
{
    public FailedRequestException(
        string content,
        string? contentType,
        HttpStatusCode statusCode,
        string errorType
    )
        : base(content)
    {
        Content = content;
        ContentType = contentType;
        StatusCode = statusCode;
        ErrorType = errorType;
    }

    protected FailedRequestException(FailedRequestException innerException)
        : base(innerException.Content, innerException)
    {
        Content = innerException.Content;
        ContentType = innerException.ContentType;
        StatusCode = innerException.StatusCode;
        ErrorType = innerException.ErrorType;
    }

    public string Content { get; }
    public string? ContentType { get; }
    public HttpStatusCode StatusCode { get; }
    public string ErrorType { get; }

    public IActionResult ToResult() =>
        new ContentResult
        {
            Content = Content,
            StatusCode = (int)StatusCode,
            ContentType = ContentType
        };
}

public class ProxiedFailedRequestException(FailedRequestException original)
    : FailedRequestException(original) { }
