using Bank.Core.Common;

namespace Bank.TransactionsGateway.App.Services;

public interface ITransactionService
{
    Task Dispatch(Transaction transaction);
}
