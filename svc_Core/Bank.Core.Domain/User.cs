using Bank.Attributes.Attributes;
using Bank.Common.Money;

namespace Bank.Core.Domain;

[ModelName("Client")]
public class User : StoredModel
{
    public List<Account> Accounts { get; set; } = [];

    public void OpenNewAccount(string name, Currency currency)
    {
        var account = new Account(this, name, currency);
        Accounts.Add(account);
    }

    public User(Guid id)
    {
        Id = id;
    }

    protected User() { }
}
