namespace Bank.Credit.Domain
{
    public class Tariff : Entity
    {
        public string Name { get; set; }
        public int Rate { get; set; }

        /// <summary>
        /// Daily rate for missed payments
        /// </summary>
        public int PenaltyRate => 1;
        public TimeSpan Period { get; } = TimeSpan.FromDays(1);

        public bool CanApplyRate(TimeSpan timeSinceLastPayment) => timeSinceLastPayment > Period;

        private Tariff() { }
        public Tariff(string name, int rate)
        {
            Name = name;
            Rate = rate;
        }
    }
}
