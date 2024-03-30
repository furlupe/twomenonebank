using System.Text.Json;
using Bank.Core.Common;
using Bank.Exceptions.WebApiException;
using MassTransit;

namespace Bank.TransactionsGateway.App.Services;

public class TransactionService(IRequestClient<Transaction> requestClient) : ITransactionService
{
    public async Task Dispatch(Transaction transaction)
    {
        var response = await requestClient.GetResponse<TransactionResponse>(transaction);
        var message = response.Message;
        if (message.Type == TransactionResponse.ResponseType.Failure)
        {
            if (message.Details is not null && message.Details.Status != 500)
                throw new FailedRequestException(message.Details);

            throw new FailedRequestException(message.Message);
        }
    }
}
