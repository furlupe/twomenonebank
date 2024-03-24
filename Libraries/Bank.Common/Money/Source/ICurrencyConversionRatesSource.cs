namespace Bank.Common.Money.Source;

public interface ICurrencyConversionRatesSource
{
    Task<Dictionary<Currency, decimal>> GetConversionRates(Currency source);
}
