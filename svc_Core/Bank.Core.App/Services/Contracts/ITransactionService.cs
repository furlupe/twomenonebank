using Bank.Common.Pagination;
using Bank.Core.App.Dto.Pagination;
using Bank.Core.Domain.Events;

namespace Bank.Core.App.Services.Contracts;

public interface ITransactionService
{
    Task<PageDto<AccountEvent>> GetAccountTransactions(
        Guid id,
        TransactionQueryParameters queryParameters
    );
    Task Perform(Common.Transaction model);
}
