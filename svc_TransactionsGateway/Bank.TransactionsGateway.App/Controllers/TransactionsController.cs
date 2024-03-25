using Bank.Core.Common;
using Bank.TransactionsGateway.App.Dto;
using Bank.TransactionsGateway.App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.TransactionsGateway.App.Controllers;

[Route("accounts")]
[ApiController]
[Authorize]
public class TransactionsController(ITransactionService transactionService) : ControllerBase
{
    [HttpPost("{id}/deposit")]
    public async Task Deposit([FromRoute] Guid id, [FromBody] DepositDto transaction)
    {
        await transactionService.Dispatch(
            new DepositTransaction() { TargetId = id, Value = transaction.Value }
        );
    }

    [HttpPost("{id}/withdraw")]
    public async Task Withdraw([FromRoute] Guid id, [FromBody] WithdrawalDto transaction)
    {
        await transactionService.Dispatch(
            new WithdrawalTransaction() { TargetId = id, Value = transaction.Value }
        );
    }
}
