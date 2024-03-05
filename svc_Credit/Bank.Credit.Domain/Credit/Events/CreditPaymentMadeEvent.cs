namespace Bank.Credit.Domain.Credit.Events
{
    public class CreditPaymentMadeEvent : CreditEvent
    {
        public int Amount { get; set; }

        private CreditPaymentMadeEvent()
            : base() { }

        public CreditPaymentMadeEvent(Guid creditId, int amount, DateTime date)
            : base(creditId, date)
        {
            Amount = amount;
        }
    }
}
