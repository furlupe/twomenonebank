using Bank.Attributes.Attributes;
using Bank.Common.Money;
using Bank.Common.Utils;

namespace Bank.Core.Domain;

[ModelName("Client")]
public class User : StoredModel
{
    public List<Account> Accounts { get; protected set; } = [];
    public Account? DefaultTransferAccount { get; protected set; }
    public Guid? DefaultTransferAccountId { get; protected set; }

    public void OpenNewAccount(string name, Currency currency, DateTime now, Guid idempotenceKey)
    {
        var account = new Account(this, name, currency, now, idempotenceKey);
        Accounts.Add(account);
    }

    public void SetDefaultTransferAccount(Account account)
    {
        ValidateAccountOwned(account);
        ValidateAccountOpen(account);
        DefaultTransferAccount = account;
    }

    public User(Guid id)
    {
        Id = id;
    }

    protected User() { }

    protected void ValidateAccountOwned(Account account) =>
        Validation.Check(
            ExceptionConstants.MsgInvalidValue,
            Accounts.Any(x => x.Id == account.Id),
            "Cannot set somebody else's account as default."
        );

    protected void ValidateAccountOpen(Account account) =>
        Validation.Check(
            ExceptionConstants.MsgInvalidAction,
            !account.IsClosed,
            "Cannot set closed account as default."
        );
}
