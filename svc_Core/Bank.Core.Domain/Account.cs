using Bank.Attributes.Attributes;
using Bank.Common.Money;
using Bank.Common.Utils;
using Bank.Core.Domain.Events;

namespace Bank.Core.Domain;

[ModelName("Account")]
public class Account : StoredModel
{
    public Account(User user, string name, Currency currency)
    {
        User = user;
        Name = name;
        Balance = new(0, currency);
    }

    public Money Balance { get; set; } = null!;
    public Guid UserId { get; protected set; }
    public User User { get; protected set; } = null!;
    public string Name { get; protected set; } = null!;
    public List<AccountEvent> Events { get; protected set; } = [];
    public bool IsMaster { get; set; } = false;
    public DateTime? ClosedAt { get; protected set; } = null;
    public bool IsClosed => ClosedAt != null;
    public bool IsDefault => User.DefaultTransferAccountId == Id;

    public void AddEvent(AccountEvent @event)
    {
        Events.Add(@event);
    }

    public void Close(DateTime now)
    {
        ValidateEmpty();
        ValidateNotClosed();
        ValidateNotDefault();
        ValidateNotMaster();
        ClosedAt = now;
    }

    public void ValidateEmpty() =>
        Validation.Check(
            ExceptionConstants.MsgInvalidAction,
            Balance.Amount == 0,
            "Cannot close a non-empty account."
        );

    public void ValidateNotDefault() =>
        Validation.Check(
            ExceptionConstants.MsgInvalidAction,
            !IsDefault,
            "Cannot close a default account."
        );

    public void ValidateNotClosed() =>
        Validation.Check(
            ExceptionConstants.MsgInvalidAction,
            !IsClosed,
            "Account is already closed."
        );

    public void ValidateNotMaster() =>
        Validation.Check(
            ExceptionConstants.MsgInvalidAction,
            !IsMaster,
            "Cannot close master account."
        );

    protected Account() { }
}
