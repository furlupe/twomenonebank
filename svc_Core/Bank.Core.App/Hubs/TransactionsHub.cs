using Bank.Auth.Common.Attributes;
using Bank.Auth.Common.Extensions;
using Bank.Core.App.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Bank.Core.App.Hubs;

[Authorize]
public class TransactionsHub(IAccountService accountService) : Hub<ITransactionsClient>
{
    [HubMethodName("subscribe")]
    public async Task SubscribeToAccountTransactions(Guid id)
    {
        await accountService.CheckAccountOwnedBy(id, Context.User!.GetId());
        await Subscribe(id);
    }

    [CalledByStaff]
    [HubMethodName("subscribe-f")]
    public Task DoSubscribeToAccountTransactions(Guid id) => Subscribe(id);

    private Task Subscribe(Guid id) => Groups.AddToGroupAsync(Context.ConnectionId, id.ToString());
}
