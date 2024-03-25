using Bank.Common.Money;

namespace Bank.TransactionsGateway.App.Dto;

public abstract class TransactionDto
{
    public Money Value { get; set; }
}

public class DepositDto : TransactionDto { }

public class WithdrawalDto : TransactionDto { }
