using Bank.Core.Domain.Transactions;

namespace Bank.Core.App.Services.Contracts;

public interface ITransactionFactory
{
    public Task<Transaction> Create(Common.Transaction transaction);
}
