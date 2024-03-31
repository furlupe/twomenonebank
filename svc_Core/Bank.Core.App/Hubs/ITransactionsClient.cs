using Bank.Core.Http.Dto.Events;

namespace Bank.Core.App.Hubs;

public interface ITransactionsClient
{
    Task ReceiveTransactions(IEnumerable<AccountEventDto> transactions);
}
