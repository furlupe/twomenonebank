using Microsoft.EntityFrameworkCore;

namespace Bank.Common.Money;

[Owned]
public record class Money(decimal Amount, Currency Currency)
{
    public static bool operator >=(Money lhs, Money rhs) =>
        CurrenciesMatch(lhs, rhs) && lhs.Amount >= rhs.Amount;

    public static bool operator <=(Money lhs, Money rhs) =>
        CurrenciesMatch(lhs, rhs) && lhs.Amount <= rhs.Amount;

    public static bool operator >(Money lhs, Money rhs) =>
        CurrenciesMatch(lhs, rhs) && lhs.Amount > rhs.Amount;

    public static bool operator <(Money lhs, Money rhs) =>
        CurrenciesMatch(lhs, rhs) && lhs.Amount < rhs.Amount;

    public static Money operator +(Money lhs) => lhs;

    public static Money operator -(Money lhs) => new(-lhs.Amount, lhs.Currency);

    public static Money operator +(Money lhs, Money rhs) =>
        new(lhs.Amount + rhs.Amount, MatchCurrencies(lhs, rhs));

    public static Money operator -(Money lhs, Money rhs) =>
        new(lhs.Amount - rhs.Amount, MatchCurrencies(lhs, rhs));

    public static Money operator *(Money lhs, decimal rhs) => new(lhs.Amount * rhs, lhs.Currency);

    public static Money operator *(decimal lhs, Money rhs) => new(lhs * rhs.Amount, rhs.Currency);

    public static Money operator /(Money lhs, decimal rhs) => new(lhs.Amount / rhs, lhs.Currency);

    private static Currency MatchCurrencies(Money lhs, Money rhs)
    {
        CurrenciesMatch(lhs, rhs);
        return lhs.Currency;
    }

    private static bool CurrenciesMatch(Money lhs, Money rhs)
    {
        if (lhs.Currency != rhs.Currency)
            throw new ArgumentException(
                "This operation can only be performed on money in the same currency, "
                    + $"but currencies of the operands were {lhs.Currency} and {rhs.Currency}."
            );
        return true;
    }

    public override string ToString()
    {
        return $"{CurrencyUtils.CurrencySymbols[Currency]}{Amount}";
    }
}

public enum Currency
{
    AFN,
    EUR,
    IQD,
    NGN,
    RUB,
    USD
}

public static class CurrencyUtils
{
    public static Dictionary<Currency, string> CurrencySymbols =>
        new()
        {
            { Currency.AFN, "؋" },
            { Currency.EUR, "€" },
            { Currency.IQD, "د.ع" },
            { Currency.NGN, "₦" },
            { Currency.RUB, "₽" },
            { Currency.USD, "$" }
        };
}
