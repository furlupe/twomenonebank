using Bank.Common.Pagination;
using Bank.Core.App.Dto.Pagination;
using Bank.Core.Domain;
using Bank.Core.Domain.Events;
using Bank.Core.Domain.Transactions;

namespace Bank.Core.App.Services.Contracts;

public interface ITransactionService
{
    Task<PageDto<AccountEvent>> GetAccountTransactions(
        Guid id,
        TransactionQueryParameters queryParameters
    );
    Task Deposit(Account source, Deposit transaction);
    Task Withdraw(Account source, Withdrawal transaction);
    Task RepayCredit(Account source, Domain.Transactions.CreditPayment transaction);
}
