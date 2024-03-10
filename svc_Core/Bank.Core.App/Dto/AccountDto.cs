using Bank.Core.Domain;

namespace Bank.Core.App.Dto;

public class AccountDto
{
    public Guid Id { get; set; }
    public long Balance { get; set; }
    public string Name { get; set; }

    public static AccountDto From(Account account) =>
        new AccountDto
        {
            Id = account.Id,
            Balance = account.Balance,
            Name = account.Name
        };
}
