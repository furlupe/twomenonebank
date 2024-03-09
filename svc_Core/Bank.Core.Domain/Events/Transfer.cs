using Bank.Common.Utils;

namespace Bank.Core.Domain.Events;

public class Transfer
{
    public Guid Id { get; set; }
    public BalanceChange Source { get; protected set; }
    public BalanceChange Target { get; protected set; }

    public Transfer(BalanceChange source, BalanceChange target)
    {
        Source = source;
        Target = target;

        ValidateCircuit();
        ValidateEquality();
    }

    protected void ValidateCircuit() =>
        Validation.Check(
            ExceptionConstants.MsgInvalidAction,
            Source.Account.Id != Target.Account.Id,
            "Source and target can't be the same account."
        );

    protected void ValidateEquality() =>
        Validation.Check(
            ExceptionConstants.MsgInvalidAction,
            Source.Value == Target.Value,
            "Withdrawal and deposit values must be equal."
        );

    protected Transfer() { }
}
