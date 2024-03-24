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
