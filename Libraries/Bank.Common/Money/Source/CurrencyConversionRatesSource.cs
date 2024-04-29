using System.Text.Json.Serialization;
using Bank.Common.Extensions;
using Bank.Exceptions.WebApiException;
using Microsoft.Extensions.Options;

namespace Bank.Common.Money.Source;

public class CurrencyConversionRatesSource(
    HttpClient client,
    IOptions<CurrencyConversionRatesSourceConfiguration> options
) : ICurrencyConversionRatesSource
{
    public async Task<Dictionary<Currency, decimal>> GetConversionRates(Currency source)
    {
        var address = options.Value.Address + source;
        var response = await client.GetAsJson<CurrencyConversionRatesDto>(address);

        if (response is null || response.Result != "success")
            throw new FailedRequestException("Could not retrieve conversion rates.");

        return response
            .ConversionRates.Where(x =>
                Enum.TryParse<Currency>(x.Key, ignoreCase: true, out var target) && target != source
            )
            .ToDictionary(x => Enum.Parse<Currency>(x.Key), x => x.Value);
    }
}

public class CurrencyConversionRatesDto
{
    [JsonPropertyName("result")]
    public string Result { get; set; } = "failure";

    [JsonPropertyName("base_code")]
    public string Source { get; set; } = null!;

    [JsonPropertyName("conversion_rates")]
    public Dictionary<string, decimal> ConversionRates { get; set; } = [];
}
