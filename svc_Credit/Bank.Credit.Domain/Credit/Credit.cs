using System.ComponentModel.DataAnnotations.Schema;
using Bank.Credit.Domain.Credit.Events;

namespace Bank.Credit.Domain.Credit
{
    public class Credit : Entity
    {
        public bool IsClosed { get; set; } = false;
        public Tariff Tariff { get; set; }
        public User User { get; set; }
        public int Amount { get; set; }
        public int BaseAmount { get; set; }
        public int Penalty { get; set; } = 0;
        public int Days { get; set; }
        public DateTime NextPaymentDate { get; set; }
        public DateTime LastPaymentDate { get; set; } = DateTime.UtcNow;
        public DateTime RateLastApplied { get; set; }
        public int MissedPaymentPeriods { get; set; } = 0;
        public int PeriodicPayment { get; set; }
        public List<CreditEvent> Events { get; set; } = [];

        private Credit() { }

        public Credit(User user, Tariff tariff, int amount, int days)
        {
            User = user;
            Tariff = tariff;

            BaseAmount = amount;
            Amount = amount;

            Days = days;

            var dailyRate = tariff.Rate / 100.0;
            PeriodicPayment = (int)
                Math.Floor(
                    amount
                        * dailyRate
                        * Math.Pow(1 + dailyRate, days)
                        / (Math.Pow(1 + dailyRate, days) - 1)
                );

            var now = DateTime.UtcNow;
            NextPaymentDate = now + Tariff.Period;
        }

        [NotMapped]
        public bool IsActive => !IsClosed && !IsDeleted;

        public void AddPenalty()
        {
            var now = DateTime.UtcNow;
            var amount =
                (int)Math.Floor(Amount * Tariff.PenaltyRate * (now - LastPaymentDate).Days / 100.0)
                - Penalty;

            if (amount < 1)
                return;

            ApplyAndRecord(new CreditPenaltyAddedEvent(Id, amount, now));
        }

        // supposed to be called each period
        public void MoveNextPaymentDate()
        {
            var now = DateTime.UtcNow;

            ApplyAndRecord(
                new CreditPaymentDateMovedEvent(Id, NextPaymentDate.Add(Tariff.Period), now)
            );

            if (now.Date - LastPaymentDate.Date > Tariff.Period)
            {
                ApplyAndRecord(new CreditPaymentMissedEvent(Id, now));
            }
        }

        public void Pay()
        {
            var @event = new CreditPaymentMadeEvent(Id, PeriodicPayment, DateTime.UtcNow);
            ApplyAndRecord(@event);
        }

        public void PayPenalty()
        {
            var @event = new CreditPenaltyPaidEvent(Id, Penalty, DateTime.UtcNow);
            ApplyAndRecord(@event);
        }

        public void ApplyRate()
        {
            var @event = new CreditRateAppliedEvent(Id, DateTime.UtcNow);
            ApplyAndRecord(@event);
        }

        public void Close()
        {
            var @event = new CreditClosedEvent(Id, DateTime.UtcNow);
            ApplyAndRecord(@event);
        }

        private void ApplyAndRecord(CreditEvent @event)
        {
            if (IsClosed)
            {
                throw new InvalidDataException("Can't perform events over closed credit");
            }

            switch (@event)
            {
                case CreditPaymentMadeEvent creditPaymentMadeEvent:
                    Apply(creditPaymentMadeEvent);
                    break;
                case CreditRateAppliedEvent creditRateAppliedEvent:
                    Apply(creditRateAppliedEvent);
                    break;
                case CreditClosedEvent creditClosedEvent:
                    Apply(creditClosedEvent);
                    break;
                case CreditPaymentDateMovedEvent creditPaymentDateMovedEvent:
                    Apply(creditPaymentDateMovedEvent);
                    break;
                case CreditPaymentMissedEvent creditPaymentMissedEvent:
                    Apply(creditPaymentMissedEvent);
                    break;
                case CreditPenaltyAddedEvent creditPenaltyAddedEvent:
                    Apply(creditPenaltyAddedEvent);
                    break;
                case CreditPenaltyPaidEvent creditPenaltyPaidEvent:
                    Apply(creditPenaltyPaidEvent);
                    break;
                default:
                    throw new InvalidDataException(
                        $"{@event.GetType()} event can't be applied to {nameof(Credit)}"
                    );
            }

            Events.Add(@event);
        }

        private void Apply(CreditPaymentMadeEvent @event)
        {
            var amountToPay = PeriodicPayment * (MissedPaymentPeriods + 1);

            if (@event.Amount < amountToPay)
            {
                throw new InvalidOperationException(
                    $"Can't withdraw, since amount is less than required. Missed payments: ${MissedPaymentPeriods}"
                );
            }

            if (@event.Amount > amountToPay)
            {
                throw new InvalidOperationException(
                    $"Can't overpay the periodic amount. What are you, some sort of richman?"
                );
            }

            MissedPaymentPeriods = 0;
            Amount -= @event.Amount > Amount ? Amount : @event.Amount;

            if (Amount == 0)
            {
                Close();
            }
        }

        private void Apply(CreditRateAppliedEvent @event)
        {
            var diff = DateTime.UtcNow.Date - RateLastApplied.Date;
            if (!Tariff.CanApplyRate(diff))
            {
                throw new InvalidDataException("Can't apply rate since it's too early to do so");
            }

            var percent = Tariff.Rate / 100.0;
            Amount += (int)Math.Floor(Amount * percent);

            RateLastApplied = @event.CreatedAt;
        }

        private void Apply(CreditPaymentDateMovedEvent @event)
        {
            if (DateTime.UtcNow.Date - NextPaymentDate.Date < Tariff.Period)
            {
                throw new InvalidOperationException("Can't move since period hasn't passed yet");
            }

            NextPaymentDate = @event.To;
        }

        private void Apply(CreditPaymentMissedEvent @event)
        {
            MissedPaymentPeriods += 1;
        }

        private void Apply(CreditPenaltyAddedEvent @event)
        {
            Penalty += @event.Amount;
        }

        private void Apply(CreditPenaltyPaidEvent @event)
        {
            Penalty = 0;
        }

        private void Apply(CreditClosedEvent @event)
        {
            IsClosed = true;
        }
    }
}
