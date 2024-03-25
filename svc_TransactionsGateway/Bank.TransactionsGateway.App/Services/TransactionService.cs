using System.Text.Json;
using Bank.Core.Common;

namespace Bank.TransactionsGateway.App.Services;

public class TransactionService : ITransactionService
{
    public async Task Dispatch(Transaction transaction)
    {
        Console.WriteLine(JsonSerializer.Serialize(transaction));
    }
}
