using Bank.Auth.Common.Attributes;
using Bank.Auth.Common.Extensions;
using Bank.Common.Http;
using Bank.Core.Common;
using Bank.TransactionsGateway.App.Services;
using Bank.TransactionsGateway.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Bank.Core.Common.Transfer;

namespace Bank.TransactionsGateway.App.Controllers;

[ApiController]
[Authorize]
public class CreditTransactionsController(ITransactionService transactionService) : ControllerBase
{
    [HttpPost("credit/reclaim/{targetId}")]
    [CalledByService]
    public async Task Reclaim([FromRoute] Guid targetId, [FromBody] CreditTransferDto transaction)
    {
        await transactionService.Dispatch(
            new(
                transaction.Value,
                Guid.Empty,
                HttpContext.GetIdempotenceKey(),
                targetId,
                transfer: new()
                {
                    TargetId = Guid.Empty,
                    Type = TransferType.Credit,
                    CreditTransfer = new() { CreditId = transaction.CreditId },
                    Message = transaction.Message
                }
            )
        );
    }

    [HttpPost("credit/give/{targetId}")]
    [CalledByService]
    public async Task Give([FromRoute] Guid targetId, [FromBody] CreditTransferDto transaction)
    {
        await transactionService.Dispatch(
            new(
                transaction.Value,
                Guid.Empty,
                HttpContext.GetIdempotenceKey(),
                Guid.Empty,
                transfer: new()
                {
                    TargetId = targetId,
                    Type = TransferType.Credit,
                    CreditTransfer = new() { CreditId = transaction.CreditId },
                    Message = transaction.Message
                }
            )
        );
    }

    [HttpPost("accounts/master/deposit")]
    [CalledByStaff]
    public async Task Deposit([FromBody] DepositDto transaction)
    {
        await transactionService.Dispatch(
            new(
                transaction.Value,
                User.GetId(),
                HttpContext.GetIdempotenceKey(),
                Guid.Empty,
                balanceChange: new() { Type = BalanceChange.BalanceChangeType.Deposit }
            )
        );
    }

    [HttpPost("accounts/master/withdraw")]
    [CalledByStaff]
    public async Task Withdraw([FromBody] WithdrawalDto transaction)
    {
        await transactionService.Dispatch(
            new(
                transaction.Value,
                User.GetId(),
                HttpContext.GetIdempotenceKey(),
                Guid.Empty,
                balanceChange: new() { Type = BalanceChange.BalanceChangeType.Withdrawal }
            )
        );
    }
}
