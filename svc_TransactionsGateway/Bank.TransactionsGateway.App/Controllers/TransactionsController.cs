using Bank.Auth.Common.Extensions;
using Bank.Auth.Http.AuthClient;
using Bank.Common.Http;
using Bank.Core.Common;
using Bank.TransactionsGateway.App.Services;
using Bank.TransactionsGateway.Http;
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
            new(
                transaction.Value,
                User.GetId(),
                HttpContext.GetIdempotenceKey(),
                sourceId,
                transfer: new()
                {
                    TargetId = await authClient.GetUserIdByPhone(transfereeIdentifier),
                    Message = transaction.Message,
                    Type = TransferType.P2P
                }
            )
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
            new(
                transaction.Value,
                User.GetId(),
                HttpContext.GetIdempotenceKey(),
                sourceId,
                transfer: new()
                {
                    TargetId = targetId,
                    Message = transaction.Message,
                    Type = TransferType.Me2Me,
                }
            )
        );
    }

    [HttpPost("{id}/deposit")]
    public async Task Deposit([FromRoute] Guid id, [FromBody] DepositDto transaction)
    {
        await transactionService.Dispatch(
            new(
                transaction.Value,
                User.GetId(),
                HttpContext.GetIdempotenceKey(),
                id,
                balanceChange: new() { Type = BalanceChange.BalanceChangeType.Deposit }
            )
        );
    }

    [HttpPost("{id}/withdraw")]
    public async Task Withdraw([FromRoute] Guid id, [FromBody] WithdrawalDto transaction)
    {
        await transactionService.Dispatch(
            new(
                transaction.Value,
                User.GetId(),
                HttpContext.GetIdempotenceKey(),
                id,
                balanceChange: new() { Type = BalanceChange.BalanceChangeType.Withdrawal }
            )
        );
    }
}
