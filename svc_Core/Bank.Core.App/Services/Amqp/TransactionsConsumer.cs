using Bank.Core.App.Services.Contracts;
using Bank.Core.Common;
using Bank.Exceptions.WebApiException;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Core.App.Services.Amqp;

public class TransactionsConsumer(ITransactionService transactionService) : IConsumer<Transaction>
{
    public async Task Consume(ConsumeContext<Transaction> context)
    {
        try
        {
            await transactionService.Perform(context.Message);
        }
        catch (Exception ex) when (ex is IWebApiException wae)
        {
            if (wae.ToResult() is ObjectResult { Value: ProblemDetails details })
                await context.RespondAsync(TransactionResponse.Failure(ex.Message, details));
            else
                await context.RespondAsync(TransactionResponse.Failure(ex.Message));
            return;
        }
        catch (Exception ex)
        {
            await context.RespondAsync(TransactionResponse.Failure(ex.Message));
            throw;
        }

        await context.RespondAsync(TransactionResponse.Success());
    }
}
