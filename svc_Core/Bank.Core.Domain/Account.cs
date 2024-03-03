using Bank.Attributes.Attributes;
using Bank.Core.Domain.Events;

namespace Bank.Core.Domain;

[ModelName("Account")]
public class Account : StoredModel
{
    public User User { get; set; }

    public long Balance { get; set; } = 0;
    public List<BalanceChange> BalanceChanges { get; set; } = [];

    public Account(User user)
    {
        User = user;
    }

    protected Account() { }
}
