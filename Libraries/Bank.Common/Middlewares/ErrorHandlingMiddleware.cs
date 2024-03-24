using System.Diagnostics;
using Bank.Exceptions;
using Bank.Exceptions.WebApiException;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;

namespace Bank.Common.Middlewares;

public class ErrorHandlingMiddleware(RequestDelegate next, IHostEnvironment env)
{
    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception e) when (e is IWebApiException wae)
        {
            await ProcessWebApiException(httpContext, wae.ToResult());
        }
        catch (Exception e) when (e is BadHttpRequestException)
        {
            throw;
        }
        catch (Exception e)
        {
            await ProcessException(httpContext, e);
        }
    }

    private static async Task ExecuteResult(HttpContext httpContext, IActionResult result)
    {
        var actionContext = new ActionContext(httpContext, httpContext.GetRouteData(), new());
        await result.ExecuteResultAsync(actionContext);
    }

    private async Task ProcessException(HttpContext httpContext, Exception ex)
    {
        string? detail = env.IsProduction() ? null : ex.ToString();
        var details = new ProblemDetails
        {
            Type = ErrorTypes.InternalServerError,
            Title = "A server error has occurred.",
            Detail = detail
        };
        AddTraceId(httpContext, details);
        var result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status500InternalServerError,
        };

        await ExecuteResult(httpContext, result);
    }

    private static void AddTraceId(HttpContext context, ProblemDetails details)
    {
        string traceId = Activity.Current?.Id ?? context.TraceIdentifier;
        Console.WriteLine($"TraceId: {traceId}");
        if (traceId is { })
        {
            details.Extensions["traceId"] = traceId;
        }
    }

    private static async Task ProcessWebApiException(HttpContext httpContext, IActionResult result)
    {
        if (result is ObjectResult { Value: ProblemDetails details })
        {
            AddTraceId(httpContext, details);
        }

        await ExecuteResult(httpContext, result);
    }
}
