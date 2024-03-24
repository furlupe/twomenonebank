using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Common.Constants;

public class JsonConfigurationDefaults
{
    public static readonly Action<JsonOptions> JsonOptions = options =>
    {
        SerializerOptions!(options.JsonSerializerOptions);
    };

    public static readonly Action<JsonSerializerOptions> SerializerOptions = options =>
    {
        options.Converters.Add(new JsonStringEnumConverter());
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    };
}
