using Bank.Common.Extensions;
using Bank.Common.Money;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Bank.TransactionsGateway.Http.Client
{
    public class TransactionsClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public TransactionsClient(HttpClient httpClient, IOptions<TransactionsClientOptions> options)
        {
            _httpClient = httpClient;
            _baseUrl = options.Value.Host;
        }

        public async Task WithdrawFromMasterAccount(Guid accountId, Guid creditId, Money amount)
        {
            var url = $"{_baseUrl}/credit/give/{accountId}";
            await _httpClient.PostAsJson(
                url, 
                new CreditTransferDto () {
                    Value = amount,
                    CreditId = creditId
                }
            );
        }

        public async Task ReclaimCreditPayment(Guid accountId, Guid creditId, Money amount)
        {
            var url = $"{_baseUrl}/credit/reclaim/{accountId}";
            await _httpClient.PostAsJson(
                url,
                new CreditTransferDto()
                {
                    Value = amount,
                    CreditId = creditId
                }
            );
        }
    }
}
