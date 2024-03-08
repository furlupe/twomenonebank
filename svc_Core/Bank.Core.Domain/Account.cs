using Bank.Attributes.Attributes;
using Bank.Common.Utils;
using Bank.Core.Domain.Events;

namespace Bank.Core.Domain;

[ModelName("Account")]
public class Account : StoredModel
{
    public User User { get; set; }

    public string Name { get; set; }
    public long Balance { get; set; } = 0;
    public List<BalanceChange> BalanceChanges { get; set; } = [];
    public List<Transfer> Transfers { get; set; } = [];

    public void ValidateClose()
     =>
        ValidationUtils.Check(ExceptionConstants.MsgInvalidAction, Balance == 0, "Cannot close a non-empty account");
    

    public Account(User user)
    {
        User = user;
    }

    protected Account() { }
}
