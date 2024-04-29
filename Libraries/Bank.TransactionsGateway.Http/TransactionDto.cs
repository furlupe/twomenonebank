using Bank.Common.Money;

namespace Bank.TransactionsGateway.Http;

public abstract class TransactionDto
{
    public Money Value { get; set; } = null!;
}

public class DepositDto : TransactionDto { }

public class WithdrawalDto : TransactionDto { }

public class TransferDto : TransactionDto
{
    public string? Message { get; set; }
}

public class CreditTransferDto : TransferDto
{
    public Guid CreditId { get; set; }
}
