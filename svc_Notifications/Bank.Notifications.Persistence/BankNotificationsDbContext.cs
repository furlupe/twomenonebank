using Bank.Notifications.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bank.Notifications.Persistence
{
    public class BankNotificationsDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public BankNotificationsDbContext(DbContextOptions<BankNotificationsDbContext> options): base(options) { }
    }
}
