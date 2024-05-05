using Bank.Common.Pagination;
using Bank.Core.Domain.Events;
using Bank.Core.Http.Dto.Pagination;

namespace Bank.Core.App.Services.Contracts;

public interface ITransactionsService
{
    Task<PageDto<TransactionEvent>> GetAccountTransactions(
        Guid id,
        TransactionQueryParameters queryParameters
    );

    Task<PageDto<TransactionEvent>> GetMasterAccountTransactions(
        TransactionQueryParameters queryParameters
    );
    Task Perform(Common.Transaction model);
}
