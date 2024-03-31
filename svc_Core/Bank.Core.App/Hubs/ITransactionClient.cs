using Bank.Core.Http.Dto.Events;

namespace Bank.Core.App.Hubs;

public interface ITransactionClient
{
    Task ReceiveTransactions(List<AccountEventDto> transactions);
}
