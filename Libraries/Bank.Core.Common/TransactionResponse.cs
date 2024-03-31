using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Core.Common;

public class TransactionResponse()
{
    public static TransactionResponse Success() => new() { Type = ResponseType.Success };

    public static TransactionResponse Failure(string message, ProblemDetails? details = null) =>
        new()
        {
            Type = ResponseType.Failure,
            Message = message,
            Details = details,
        };

    public ResponseType Type { get; init; }
    public string? Message { get; init; }
    public SerializableProblemDetails? Details { get; init; }

    public enum ResponseType
    {
        Success,
        Failure
    }
}

/// <summary>
/// https://github.com/MassTransit/MassTransit/discussions/4802
/// </summary>
public class SerializableProblemDetails
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyOrder(-5)]
    public string? Type { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyOrder(-4)]
    public string? Title { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyOrder(-3)]
    public int? Status { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyOrder(-2)]
    public string? Detail { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyOrder(-1)]
    public string? Instance { get; set; }
    public IDictionary<string, object?> Extensions { get; set; } =
        new Dictionary<string, object?>(StringComparer.Ordinal);

    public static implicit operator ProblemDetails(SerializableProblemDetails details) =>
        new ProblemDetails
        {
            Extensions = details.Extensions,
            Detail = details.Detail,
            Instance = details.Instance,
            Status = details.Status,
            Title = details.Title,
            Type = details.Type,
        };

    public static implicit operator SerializableProblemDetails?(ProblemDetails? details) =>
        details is null
            ? null
            : new SerializableProblemDetails
            {
                Extensions = details.Extensions,
                Detail = details.Detail,
                Instance = details.Instance,
                Status = details.Status,
                Title = details.Title,
                Type = details.Type,
            };
}
