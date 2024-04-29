using Bank.Attributes.Attributes;

namespace Bank.Common.Money.Source;

[ConfigurationModel("CurrencyConversionRatesSource")]
public class CurrencyConversionRatesSourceConfiguration
{
    public string Address { get; set; } = null!;
}
