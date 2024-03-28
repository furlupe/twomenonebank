using Bank.Auth.Common.Attributes;
using Bank.Auth.Common.Extensions;
using Bank.Auth.Http.AuthClient;
using Bank.Core.Common;
using Bank.TransactionsGateway.App.Dto;
using Bank.TransactionsGateway.App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.TransactionsGateway.App.Controllers;

[Route("accounts")]
[ApiController]
[Authorize]
public class TransactionsController(ITransactionService transactionService, AuthClient authClient)
    : ControllerBase
{
    [HttpPost("{id}/transfer")]
    public async Task Transfer([FromRoute] Guid id, [FromBody] TransferDto transaction)
    {
        await transactionService.Dispatch(
            new()
            {
                Value = transaction.Value,
                InitiatorId = User.GetId(),
                Type = Transaction.TransactionType.Transfer,
                Transfer = new()
                {
                    TargetAccountId = id,
                    SourceAccountId = await authClient.GetUserIdByPhone(
                        transaction.TransfereeIdentifier
                    ),
                }
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
                Type = Transaction.TransactionType.BalanceChange,
                BalanceChange = new()
                {
                    TargetAccountId = id,
                    Type = BalanceChange.BalanceChangeType.Deposit
                }
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
                Type = Transaction.TransactionType.BalanceChange,
                BalanceChange = new()
                {
                    TargetAccountId = id,
                    Type = BalanceChange.BalanceChangeType.Withdrawal
                }
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
                Type = Transaction.TransactionType.BalanceChange,
                BalanceChange = new()
                {
                    TargetAccountId = id,
                    Type = BalanceChange.BalanceChangeType.CreditPayment,
                    CreditPayment = new() { CreditId = transaction.CreditId }
                }
            }
        );
    }
}
