using Bank.Credit.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Bank.Credit.App.Services
{
    public class CreditBackroundService
    {
        private readonly BankCreditDbContext _dbContext;
        private const int Gap = 100;

        public CreditBackroundService(BankCreditDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ProcessOpenCredits()
        {
            Console.WriteLine("Start processing credits");
            int creditsTotal = await _dbContext.Credits.CountAsync(x =>
                !x.IsDeleted && !x.IsClosed
            );

            for (int index = 0; index < creditsTotal; index += Gap)
            {
                await _dbContext
                    .Credits.Where(credit => credit.IsActive())
                    .Skip(index)
                    .Take(Gap)
                    .ForEachAsync(credit =>
                    {
                        credit.MoveNextPaymentDate();
                        credit.ApplyRate();
                        credit.AddPenalty();
                    });
            }
            Console.WriteLine("Credits processed");
        }
    }
}
