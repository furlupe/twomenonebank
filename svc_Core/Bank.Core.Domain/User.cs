using Bank.Attributes.Attributes;

namespace Bank.Core.Domain;

[ModelName("Client")]
public class User : StoredModel
{
    public List<Account> Accounts { get; set; } = [];

    public void OpenNewAccount(string name)
    {
        var account = new Account(this, name);
        Accounts.Add(account);
    }

    public User(Guid id)
    {
        Id = id;
    }

    protected User() { }
}
