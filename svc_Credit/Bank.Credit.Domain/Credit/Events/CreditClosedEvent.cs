namespace Bank.Credit.Domain.Credit.Events
{
    public class CreditClosedEvent : CreditEvent
    {
        private CreditClosedEvent()
            : base() { }

        public CreditClosedEvent(Guid aggregateId, DateTime dateTime)
            : base(aggregateId, dateTime) { }
    }
}
