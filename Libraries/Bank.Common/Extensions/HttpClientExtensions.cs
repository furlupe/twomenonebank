using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Bank.Common.Constants;
using Bank.Exceptions.WebApiException;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Common.Extensions;

public static class HttpClientExtensions
{
    private const string mediaType = MediaTypeNames.Application.Json;

    private static JsonSerializerOptions GetOptions()
    {
        var options = new JsonSerializerOptions();
        JsonConfigurationDefaults.SerializerOptions(options);
        return options;
    }

    public static async Task<T?> PostAsJson<T>(this HttpClient client, string url, object data)
    {
        string json = JsonSerializer.Serialize(data, GetOptions());
        var content = new StringContent(json, Encoding.UTF8, mediaType);

        using (HttpResponseMessage response = await client.PostAsync(url, content))
        {
            return await ReadResponse<T>(response);
        }
    }

    public static async Task PostAsJson(this HttpClient client, string url, object data)
    {
        string json = JsonSerializer.Serialize(data, GetOptions());
        var content = new StringContent(json, Encoding.UTF8, mediaType);

        using (HttpResponseMessage response = await client.PostAsync(url, content))
        {
            await response.EnsureSuccess();
        }
    }

    public static async Task<T?> PutAsJson<T>(this HttpClient client, string url, object data)
    {
        string json = JsonSerializer.Serialize(data, GetOptions());
        var content = new StringContent(json, Encoding.UTF8, mediaType);

        using (HttpResponseMessage response = await client.PutAsync(url, content))
        {
            return await ReadResponse<T>(response);
        }
    }

    public static async Task PutAsJson(this HttpClient client, string url, object data)
    {
        string json = JsonSerializer.Serialize(data, GetOptions());
        var content = new StringContent(json, Encoding.UTF8, mediaType);

        using (HttpResponseMessage response = await client.PutAsync(url, content))
        {
            await response.EnsureSuccess();
        }
    }

    public static async Task<T?> GetAsJson<T>(this HttpClient client, string url)
    {
        using (HttpResponseMessage response = await client.GetAsync(url))
        {
            return await ReadResponse<T>(response);
        }
    }

    private static async Task<T?> ReadResponse<T>(HttpResponseMessage response)
    {
        await response.EnsureSuccess();
        string body = await response.Content.ReadAsStringAsync();

        T? result;
        try
        {
            result = JsonSerializer.Deserialize<T>(body, GetOptions());
        }
        catch (Exception e)
        {
            throw new InvalidOperationException(
                $"Could not read the response. {e.Message} Content: '{body}'.",
                e
            );
        }

        return result;
    }

    public static async Task EnsureSuccess(this HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        try
        {
            string detail = await response.Content.ReadAsStringAsync();
            var original = JsonSerializer.Deserialize<ProblemDetails>(detail, GetOptions());
            if (original is not null)
                throw new FailedRequestException(original, response);
        }
        catch (Exception e) when (e is JsonException || e is NotSupportedException) { }

        throw new FailedRequestException(response);
    }
}
