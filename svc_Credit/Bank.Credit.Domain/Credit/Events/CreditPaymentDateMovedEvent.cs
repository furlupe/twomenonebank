﻿namespace Bank.Credit.Domain.Credit.Events
{
    public class CreditPaymentDateMovedEvent : CreditEvent
    {
        public DateTime To { get; }

        private CreditPaymentDateMovedEvent()
            : base() { }

        public CreditPaymentDateMovedEvent(Guid aggregateId, DateTime to, DateTime happendAt)
            : base(CreditEventType.PaymentDateMoved, aggregateId, happendAt)
        {
            To = to;
        }
    }
}
