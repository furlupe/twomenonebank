using Microsoft.AspNetCore.Mvc;

namespace Bank.Exceptions.WebApiException;

public abstract class WebApiException<TDetails>
    : ApplicationException,
        IProblemDetailsException<TDetails>
    where TDetails : ProblemDetails, new()
{
    protected WebApiException(string type, int statusCode, string? title, string? detail = null)
        : base($"{title} {detail}")
    {
        Details ??= new();

        if (!Uri.TryCreate(type, UriKind.Absolute, out Uri? _))
            type = "about:blank";

        if (string.IsNullOrEmpty(title))
            title = GetType().Name;

        Details.Type = type;
        Details.Status = statusCode;
        Details.Title = title;
        Details.Detail = detail;
    }

    public TDetails Details { get; protected set; }
}

public abstract class WebApiException : WebApiException<ProblemDetails>
{
    protected WebApiException(string type, int statusCode, string? title, string? detail = null)
        : base(type, statusCode, title, detail) { }
}
