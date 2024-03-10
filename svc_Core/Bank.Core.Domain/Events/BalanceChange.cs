using Bank.Common.Utils;

namespace Bank.Core.Domain.Events;

public class BalanceChange
{
    public long Value { get; protected set; }
    public Account Account { get; protected set; }

    public Type EventType { get; protected set; }
    public CreditPayment? CreditPayment { get; protected set; }

    public enum Type
    {
        Withdrawal,
        Deposit,
        CreditPayment
    }

    public BalanceChange(Account account, long value, CreditPayment creditPayment)
        : this(account, value, Type.CreditPayment)
    {
        CreditPayment = creditPayment;
    }

    public BalanceChange(Account account, long value, Type type)
    {
        Account = account;
        Value = value;
        EventType = type;

        ValidateValue();
        ValidateType();
    }

    protected void ValidateValue() =>
        Validation.Check(
            ExceptionConstants.MsgInvalidAction,
            Value > 0,
            "Balance change value must be positive."
        );

    protected void ValidateType() =>
        Validation.Check(
            ExceptionConstants.MsgInvalidAction,
            EventType == Type.CreditPayment && CreditPayment != null,
            "Balance change that is credit payment must have corresponding data."
        );

    protected BalanceChange() { }
}
