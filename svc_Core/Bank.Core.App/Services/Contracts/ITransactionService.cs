using Bank.Common.Pagination;
using Bank.Core.Domain.Events;
using Bank.Core.Http.Dto.Pagination;

namespace Bank.Core.App.Services.Contracts;

public interface ITransactionService
{
    Task<PageDto<AccountEvent>> GetAccountTransactions(
        Guid id,
        TransactionQueryParameters queryParameters
    );

    Task<PageDto<AccountEvent>> GetMasterAccountTransactions(
        TransactionQueryParameters queryParameters
    );
    Task Perform(Common.Transaction model);
}
