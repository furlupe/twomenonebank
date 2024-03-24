using Bank.Core.App.Dto.Events;

namespace Bank.Core.App.Hubs;

public interface ITransactionClient
{
    Task ReceiveTransactions(List<AccountEventDto> transactions);
}
