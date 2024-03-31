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

    public Money Balance { get; set; }
    public Guid UserId { get; protected set; }
    public User User { get; protected set; }
    public string Name { get; protected set; }
    public List<AccountEvent> Events { get; protected set; } = [];
    public bool IsMaster { get; set; } = false;

    public void AddEvent(AccountEvent @event)
    {
        Events.Add(@event);
    }

    public void ValidateClose() =>
        Validation.Check(
            ExceptionConstants.MsgInvalidAction,
            Balance.Amount == 0,
            "Cannot close a non-empty account"
        );

    protected Account() { }
}
