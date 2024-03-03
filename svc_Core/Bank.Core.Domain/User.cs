using Bank.Attributes.Attributes;

namespace Bank.Core.Domain;

[ModelName("Client")]
public class User : StoredModel
{
    public List<Account> Accounts { get; set; } = [];

    public Guid AddAccount()
    {
        var account = new Account(this);
        Accounts.Add(account);

        return account.Id;
    }

    public User(Guid id)
    {
        Id = id;
    }
    protected User() { }
}
