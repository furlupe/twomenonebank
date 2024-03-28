using Bank.Common.Money;

namespace Bank.TransactionsGateway.App.Dto;

public abstract class TransactionDto
{
    public Money Value { get; set; }
}

public class DepositDto : TransactionDto { }

public class WithdrawalDto : TransactionDto { }

public class CreditPaymentDto : TransactionDto
{
    public Guid CreditId { get; set; }
}

public class TransferDto : TransactionDto
{
    public string TransfereeIdentifier { get; set; }
}
