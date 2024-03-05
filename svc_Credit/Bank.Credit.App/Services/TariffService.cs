using Bank.Credit.App.Dto;
using Bank.Credit.Domain;
using Bank.Credit.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Bank.Credit.App.Services
{
    public class TariffService
    {
        private readonly BankCreditDbContext _dbContext;
        private const int PageSize = 5;

        public TariffService(BankCreditDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PageDto<TariffDto>> GetTariffs(int page)
        {
            if (page < 1)
            {
                throw new InvalidDataException($"Page cannot be less than one");
            }

            var totalPages = (int)
                Math.Ceiling(await _dbContext.Tariffs.CountAsync() / (float)PageSize);

            var tariffs = await _dbContext
                .Tariffs.Skip((page - 1) * PageSize)
                .Take(PageSize)
                .Select(t => new TariffDto()
                {
                    Id = t.Id,
                    Name = t.Name,
                    Rate = t.Rate
                })
                .ToListAsync();

            if (tariffs.Count < 1)
            {
                throw new InvalidOperationException($"Page too big");
            }

            return new()
            {
                Values = tariffs,
                Current = page,
                Total = totalPages,
                Size = tariffs.Count
            };
        }

        public async Task CreateTariff(CreateTariffDto dto)
        {
            var tariff = new Tariff(dto.Name, dto.Rate);
            await _dbContext.Tariffs.AddAsync(tariff);
            await _dbContext.SaveChangesAsync();
        }
    }
}
