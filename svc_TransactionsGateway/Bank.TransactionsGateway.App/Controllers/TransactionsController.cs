using Bank.Auth.Common.Attributes;
using Bank.Auth.Common.Extensions;
using Bank.Auth.Http.AuthClient;
using Bank.Core.Common;
using Bank.TransactionsGateway.App.Dto;
using Bank.TransactionsGateway.App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Bank.Core.Common.Transfer;

namespace Bank.TransactionsGateway.App.Controllers;

[Route("accounts")]
[ApiController]
[Authorize]
public class TransactionsController(ITransactionService transactionService, AuthClient authClient)
    : ControllerBase
{
    [HttpPost("{sourceId}/transfer/p2p/{transfereeIdentifier}")]
    public async Task TransferP2P(
        [FromRoute] Guid sourceId,
        string transfereeIdentifier,
        [FromBody] TransferDto transaction
    )
    {
        await transactionService.Dispatch(
            new()
            {
                Value = transaction.Value,
                InitiatorId = User.GetId(),
                SourceAccountId = sourceId,
                Type = Transaction.TransactionType.Transfer,
                Transfer = new()
                {
                    TargetUserId = await authClient.GetUserIdByPhone(transfereeIdentifier),
                    Type = TransferType.p2p
                }
            }
        );
    }

    [HttpPost("{sourceId}/transfer/me2me/{targetId}")]
    public async Task TransferMe2Me(
        [FromRoute] Guid sourceId,
        [FromRoute] Guid targetId,
        [FromBody] TransferDto transaction
    )
    {
        await transactionService.Dispatch(
            new()
            {
                Value = transaction.Value,
                InitiatorId = User.GetId(),
                SourceAccountId = sourceId,
                Type = Transaction.TransactionType.Transfer,
                Transfer = new() { TargetUserId = targetId, Type = TransferType.me2me }
            }
        );
    }

    [HttpPost("{id}/deposit")]
    public async Task Deposit([FromRoute] Guid id, [FromBody] DepositDto transaction)
    {
        await transactionService.Dispatch(
            new()
            {
                Value = transaction.Value,
                InitiatorId = User.GetId(),
                SourceAccountId = id,
                Type = Transaction.TransactionType.BalanceChange,
                BalanceChange = new() { Type = BalanceChange.BalanceChangeType.Deposit }
            }
        );
    }

    [HttpPost("{id}/withdraw")]
    public async Task Withdraw([FromRoute] Guid id, [FromBody] WithdrawalDto transaction)
    {
        await transactionService.Dispatch(
            new()
            {
                Value = transaction.Value,
                InitiatorId = User.GetId(),
                SourceAccountId = id,
                Type = Transaction.TransactionType.BalanceChange,
                BalanceChange = new() { Type = BalanceChange.BalanceChangeType.Withdrawal }
            }
        );
    }

    [HttpPost("{id}/repay")]
    [CalledByService]
    public async Task RepayCredit([FromRoute] Guid id, [FromBody] CreditPaymentDto transaction)
    {
        await transactionService.Dispatch(
            new()
            {
                Value = transaction.Value,
                InitiatorId = User.GetId(),
                SourceAccountId = id,
                Type = Transaction.TransactionType.BalanceChange,
                BalanceChange = new()
                {
                    Type = BalanceChange.BalanceChangeType.CreditPayment,
                    CreditPayment = new() { CreditId = transaction.CreditId }
                }
            }
        );
    }
}
