using Microsoft.AspNetCore.Mvc;

namespace Bank.Exceptions;

public interface IWebApiException { }

public interface IProblemDetailsException<TDetails> : IWebApiException
    where TDetails : ProblemDetails
{
    public TDetails Details { get; }
}
