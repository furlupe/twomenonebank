using Bank.Credit.App.Dto;
using Bank.Credit.Persistance;
using Microsoft.EntityFrameworkCore;
using Credits = Bank.Credit.Domain.Credit;

namespace Bank.Credit.App.Services
{
    public class CreditService
    {
        private readonly BankCreditDbContext _dbContext;

        public CreditService(BankCreditDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Guid userId, CreateCreditDto dto)
        {
            var user = await _dbContext.Users.SingleAsync(x => x.Id == userId);
            var tariff = await _dbContext.Tariffs.SingleAsync(x => x.Id == dto.TariffId);

            await _dbContext.Credits.AddAsync(
                new Credits.Credit(user, tariff, dto.Amount, dto.Days)
            );
            await _dbContext.SaveChangesAsync();
        }
    }
}
