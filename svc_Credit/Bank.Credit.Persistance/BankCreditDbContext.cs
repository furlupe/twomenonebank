using Bank.Credit.Domain;
using Bank.Credit.Domain.Credit.Events;
using Microsoft.EntityFrameworkCore;

namespace Bank.Credit.Persistance
{
    public class BankCreditDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }
        public DbSet<Domain.Credit.Credit> Credits { get; set; }
        public DbSet<CreditEvent> CreditEvents { get; set; }
        DbSet<CreditClosedEvent> creditClosedEvents { get; set; }
        DbSet<CreditPaymentMadeEvent> creditPaymentMadeEvents { get; set; }
        DbSet<CreditRateAppliedEvent> creditRateAppliedEvents { get; set; }
        DbSet<CreditPaymentMissedEvent> creditPaymentMissedEvents { get; set; }
        DbSet<CreditPenaltyAddedEvent> creditPenaltyAddedEvents { get; set; }
        DbSet<CreditPaymentDateMovedEvent> creditPaymentDateMovedEvents { get; set; }
        DbSet<CreditPenaltyPaidEvent> creditPenaltyPaidEvents { get; set; }

        public BankCreditDbContext(DbContextOptions<BankCreditDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tariff>().HasIndex(x => x.Name).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
