using Bank.Common.Utils;

namespace Bank.Core.Domain.Events;

public class BalanceChange
{
    public long Value { get; protected set; }
    public Account Account { get; protected set; }
    public Guid AccountId { get; protected set; }

    public BalanceChangeType EventType { get; protected set; }
    public CreditPayment? CreditPayment { get; protected set; }

    public BalanceChange(Account account, long value, CreditPayment creditPayment)
        : this(account, value, BalanceChangeType.CreditPayment)
    {
        CreditPayment = creditPayment;
        ValidateType();
    }

    public BalanceChange(Account account, long value, BalanceChangeType type)
    {
        Account = account;
        Value = value;
        EventType = type;

        ValidateValue();
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
            (EventType == BalanceChangeType.CreditPayment) == (CreditPayment != null),
            "Balance change that is credit payment must have corresponding data."
        );

    protected BalanceChange() { }
}

public enum BalanceChangeType
{
    Withdrawal,
    Deposit,
    CreditPayment
}
