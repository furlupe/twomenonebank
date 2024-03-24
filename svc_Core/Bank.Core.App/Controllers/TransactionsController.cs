using Bank.Auth.Common.Policies;
using Bank.Core.App.Services.Contracts;
using Bank.Core.Domain.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Core.App.Controllers;

[Route("accounts")]
[Authorize(Policy = Policies.CreateUserIfNeeded)]
public class TransactionsController(ITransactionService transactionService) : ControllerBase
{
    [HttpPost("{id}/deposit")]
    public async Task Deposit([FromRoute] Guid id, [FromBody] Deposit transaction)
    {
        await transactionService.Deposit(id, transaction);
    }

    [HttpPost("{id}/withdraw")]
    public async Task Withdraw([FromRoute] Guid id, [FromBody] Withdraw transaction)
    {
        await transactionService.Withdraw(id, transaction);
    }

    [HttpPost("{id}/repay")]
    public async Task RepayCredit([FromRoute] Guid id, [FromBody] RepayCredit transaction)
    {
        await transactionService.RepayCredit(id, transaction);
    }
}
