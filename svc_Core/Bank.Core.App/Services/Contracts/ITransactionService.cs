using Bank.Common.Pagination;
using Bank.Core.App.Dto.Pagination;
using Bank.Core.Domain.Events;
using Bank.Core.Domain.Transactions;

namespace Bank.Core.App.Services.Contracts;

public interface ITransactionService
{
    Task<PageDto<AccountEvent>> GetAccountTransactions(
        Guid id,
        TransactionQueryParameters queryParameters
    );
    Task Deposit(Guid accountId, Deposit dto);
    Task Withdraw(Guid accountId, Withdraw dto);
    Task RepayCredit(Guid accountId, RepayCredit dto);
}
