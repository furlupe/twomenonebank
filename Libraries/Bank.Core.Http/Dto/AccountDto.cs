using Bank.Common.Money;
using Bank.Core.Domain;

namespace Bank.Core.Http.Dto;

public class AccountDto
{
    public Guid Id { get; set; }
    public Money Balance { get; set; }
    public string Name { get; set; }
    public bool IsDefault { get; set; }
    public bool IsClosed { get; set; }

    public static AccountDto From(Account account) =>
        new AccountDto
        {
            Id = account.Id,
            Balance = account.Balance,
            Name = account.Name,
            IsDefault = account.IsDefault,
            IsClosed = account.IsClosed,
        };
}
