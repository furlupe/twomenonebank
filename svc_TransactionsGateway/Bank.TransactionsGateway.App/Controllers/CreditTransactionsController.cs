using Bank.Auth.Common.Attributes;
using Bank.Auth.Common.Extensions;
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
    //[CalledByService]
    public async Task Reclaim([FromRoute] Guid targetId, [FromBody] CreditTransferDto transaction)
    {
        await transactionService.Dispatch(
            new()
            {
                Value = transaction.Value,
                InitiatorId = Guid.Empty,
                SourceId = targetId,
                Type = Transaction.TransactionType.Transfer,
                Transfer = new()
                {
                    TargetId = Guid.Empty,
                    Type = TransferType.Credit,
                    CreditTransfer = new() { CreditId = transaction.CreditId },
                    Message = transaction.Message
                }
            }
        );
    }

    [HttpPost("credit/give/{targetId}")]
    //[CalledByService]
    public async Task Give([FromRoute] Guid targetId, [FromBody] CreditTransferDto transaction)
    {
        await transactionService.Dispatch(
            new()
            {
                Value = transaction.Value,
                InitiatorId = Guid.Empty,
                SourceId = Guid.Empty,
                Type = Transaction.TransactionType.Transfer,
                Transfer = new()
                {
                    TargetId = targetId,
                    Type = TransferType.Credit,
                    CreditTransfer = new() { CreditId = transaction.CreditId },
                    Message = transaction.Message
                }
            }
        );
    }

    [HttpPost("accounts/master/deposit")]
    [CalledByStaff]
    public async Task Deposit([FromBody] DepositDto transaction)
    {
        await transactionService.Dispatch(
            new()
            {
                Value = transaction.Value,
                InitiatorId = User.GetId(),
                SourceId = Guid.Empty,
                Type = Transaction.TransactionType.BalanceChange,
                BalanceChange = new() { Type = BalanceChange.BalanceChangeType.Deposit }
            }
        );
    }

    [HttpPost("accounts/master/withdraw")]
    [CalledByStaff]
    public async Task Withdraw([FromBody] WithdrawalDto transaction)
    {
        await transactionService.Dispatch(
            new()
            {
                Value = transaction.Value,
                InitiatorId = User.GetId(),
                SourceId = Guid.Empty,
                Type = Transaction.TransactionType.BalanceChange,
                BalanceChange = new() { Type = BalanceChange.BalanceChangeType.Withdrawal }
            }
        );
    }
}
