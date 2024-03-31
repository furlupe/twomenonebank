using Bank.Credit.Domain.Credit.Events;

namespace Bank.Credit.App.Dto
{
    public class CreditSmallDto
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public string Tariff { get; set; }
        public int Days { get; set; }
        public bool IsClosed { get; set; }
    }

    public class CreditDto
    {
        public Guid Id { get; set; }
        public TariffDto Tariff { get; set; }
        public int Amount { get; set; }
        public int BaseAmount { get; set; }
        public int Days { get; set; }
        public int Penalty { get; set; }
        public int PeriodicPayment { get; set; }
        public bool IsClosed { get; set; }
    }

    public class CreateCreditDto
    {
        public Guid TariffId { get; set; }
        public Guid WithdrawalAccountId { get; set; }
        public Guid DestinationAccountId { get; set; }
        public int Amount { get; set; }
        public int Days { get; set; }
    }

    public class CreditPaymentDto
    {
        public int Amount { get; set; }
    }

    public class CreditOperationDto
    {
        public Guid Id { get; set; }
        public Guid CreditId { get; set; }
        public CreditEventType Type { get; set; }
        public DateTime HappenedAt { get; set; }

        /// <summary>
        /// Field from <see cref="CreditPaymentDateMovedEvent"/>
        /// </summary>
        public DateTime? To { get; set; }

        /// <summary>
        /// Field from various events that contain money transfering, e.g. <see cref="CreditPaymentMadeEvent"/>
        /// </summary>
        public int? Amount { get; set; }
    }
}
