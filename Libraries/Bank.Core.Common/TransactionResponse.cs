using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static Bank.Core.Common.TransactionResponse;

namespace Bank.Core.Common;

public class TransactionResponse
{
    public static TransactionResponse Success() => new() { Type = ResponseType.Success };

    public static TransactionResponse Failure(string message, ProblemDetails? details = null) =>
        new()
        {
            Type = ResponseType.Failure,
            Message = message,
            Details = details
        };

    public ResponseType Type { get; init; }
    public string? Message { get; init; }
    public ProblemDetails? Details { get; init; }

    public enum ResponseType
    {
        Success,
        Failure
    }
}
