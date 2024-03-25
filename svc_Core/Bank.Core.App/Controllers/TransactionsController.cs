using Bank.Auth.Common.Extensions;
using Bank.Core.App.Services.Contracts;
using Bank.Core.Domain.Transactions;
using Bank.Exceptions.WebApiException;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Core.App.Controllers;

[Route("accounts")]
[ApiController]
[Authorize]
public class TransactionsController(
    IAccountService accountService,
    ITransactionService transactionService
) : ControllerBase
{
    [HttpPost("{id}/deposit")]
    public async Task Deposit([FromRoute] Guid id, [FromBody] Deposit transaction)
    {
        var account = await accountService.GetAccount(id);
        if (account.UserId != User.GetId())
            throw new NotFoundException();

        await transactionService.Deposit(account, transaction);
    }

    [HttpPost("{id}/withdraw")]
    public async Task Withdraw([FromRoute] Guid id, [FromBody] Withdrawal transaction)
    {
        var account = await accountService.GetAccount(id);
        if (account.UserId != User.GetId())
            throw new NotFoundException();

        await transactionService.Withdraw(account, transaction);
    }
}
