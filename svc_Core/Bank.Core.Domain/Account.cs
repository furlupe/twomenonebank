using System.Linq.Expressions;
using Bank.Attributes.Attributes;
using Bank.Common.Money;
using Bank.Common.Utils;
using Bank.Core.Domain.Events;

namespace Bank.Core.Domain;

[ModelName("Account")]
public class Account : StoredModel
{
    public Account(User user, string name, Currency currency, DateTime now, Guid idempotenceKey)
    {
        User = user;
        Name = name;
        Balance = new(0, currency);
        Events.Add(new AccountEvent(now, idempotenceKey, AccountEventType.Open));
    }

    public Money Balance { get; set; } = null!;
    public Guid OwnerId { get; protected set; }
    public User User { get; protected set; } = null!;
    public string Name { get; protected set; } = null!;
    public List<TransactionEvent> Transactions { get; protected set; } = [];
    public List<AccountEvent> Events { get; protected set; } = [];
    public bool IsMaster { get; set; } = false;
    public DateTime? ClosedAt { get; protected set; } = null;
    public bool IsClosed => ClosedAt != null;
    public bool IsDefault => User.DefaultTransferAccountId == Id;

    public void AddTransaction(TransactionEvent @event)
    {
        Transactions.Add(@event);
    }

    public void Close(DateTime now, Guid idempotenceKey)
    {
        ValidateEmpty();
        ValidateNotClosed();
        ValidateNotDefault();
        ValidateNotMaster();
        ClosedAt = now;
        Events.Add(new(now, idempotenceKey, AccountEventType.Close));
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

    public static Expression<Func<Account, bool>> HasId(Guid id) => x => x.Id == id;

    public static Expression<Func<Account, bool>> HasOwnerAndName(Guid ownerId, string name) =>
        x => x.OwnerId == ownerId && x.Name == name;

    protected Account() { }
}
