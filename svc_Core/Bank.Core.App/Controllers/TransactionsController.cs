using Bank.Auth.Shared.Extensions;
using Bank.Core.App.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Core.App.Controllers;

[Route("accounts")]
[Authorize]
public class TransactionsController(IAccountService accountService) : ControllerBase
{
    [HttpPost("{id}/deposit")]
    public async Task Deposit() { }

    [HttpPost("{id}/withdraw")]
    public async Task Withdraw() { }

    [HttpPost("{id}/transfer")]
    public async Task Transfer() { }
}
