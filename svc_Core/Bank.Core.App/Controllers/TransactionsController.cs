using Bank.Auth.Shared.Extensions;
using Bank.Core.App.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Core.App.Controllers;

[Route("accounts")]
[Authorize]
public class TransactionsController(IUserService userService, IAccountService accountService) : ControllerBase
{

    [HttpPost("{id}/deposit")]
    public async Task Deposit()
    {
        var user = await userService.GetUser(User.GetId());
    }

    [HttpPost("{id}/withdraw")]

    public async Task Withdraw()
    {
        var user = await userService.GetUser(User.GetId());
    }

    [HttpPost("{id}/transfer")]

    public async Task Transfer()
    {
        var user = await userService.GetUser(User.GetId());
    }
}
