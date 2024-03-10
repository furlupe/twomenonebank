using Bank.Core.Domain.Transactions;

namespace Bank.Core.App.Services.Contracts;

public interface ITransactionService
{
    public Task Deposit(Guid accountId, Deposit dto);
    public Task Withdraw(Guid accountId, Withdraw dto);
    public Task RepayCredit(Guid accountId, RepayCredit dto);
}
