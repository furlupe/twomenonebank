namespace Bank.Common.Money.Converter;

public interface ICurrencyConverter
{
    public Task<Money> Convert(Money value, Currency target);
    public Task<decimal> GetConversionRate(Currency source, Currency target);
}
