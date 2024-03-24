using Bank.Core.App.Services.Contracts;
using Bank.Core.Domain.Transactions;
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
        await transactionService.Deposit(await accountService.GetAccount(id), transaction);
    }

    [HttpPost("{id}/withdraw")]
    public async Task Withdraw([FromRoute] Guid id, [FromBody] Withdrawal transaction)
    {
        await transactionService.Withdraw(await accountService.GetAccount(id), transaction);
    }
}
