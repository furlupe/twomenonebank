using Bank.Auth.Common.Extensions;
using Bank.Core.App.Services.Contracts;
using Bank.Core.Domain;
using Bank.Exceptions.WebApiException;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Bank.Core.App.Hubs;

public class TransactionHub(IAccountService accountService) : Hub<ITransactionClient>
{
    [Authorize]
    public async Task SubscribeToAccountTransactions(Guid id)
    {
        if (await accountService.IsAccountOwnedBy(id, Context.User!.GetId()))
            throw NotFoundException.ForModel<Account>(id);

        await Subscribe(id);
    }

    [Authorize()]
    public Task DoSubscribeToAccountTransactions(Guid id) => Subscribe(id);

    private Task Subscribe(Guid id) =>
        Groups.AddToGroupAsync(Context.ConnectionId, GetAccountGroupName(id));

    private static string GetAccountGroupName(Guid accountId) => $"account/{accountId}";
}
