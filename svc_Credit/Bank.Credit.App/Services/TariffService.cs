using Bank.Common.Pagination;
using Bank.Credit.App.Dto;
using Bank.Credit.Domain;
using Bank.Credit.Persistance;

namespace Bank.Credit.App.Services
{
    public class TariffService
    {
        private readonly BankCreditDbContext _dbContext;

        public TariffService(BankCreditDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<PageDto<TariffDto>> GetTariffs(int page) =>
            _dbContext.Tariffs.GetPage(
                new() { PageNumber = page },
                t => new TariffDto()
                {
                    Id = t.Id,
                    Name = t.Name,
                    Rate = t.Rate
                }
            );

        public async Task CreateTariff(CreateTariffDto dto)
        {
            var tariff = new Tariff(dto.Name, dto.Rate);
            await _dbContext.Tariffs.AddAsync(tariff);
            await _dbContext.SaveChangesAsync();
        }
    }
}
