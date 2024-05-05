using FirebaseAdmin.Messaging;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Bank.Notifications.Http.Client
{
    public class NotificationsClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _rootUrl;

        public NotificationsClient(HttpClient httpClient, IOptions<NotificationsClientOptions> options)
        {
            _httpClient = httpClient;
            _rootUrl = options.Value.Host;
        }

        public Task NotifyEmployees(IEnumerable<Notification> messages)
            => ProcessRequest(() => _httpClient.PostAsJsonAsync($"{_rootUrl}/api/notification/employee", messages));

        public Task NotifyCustomer(Guid customerId, IEnumerable<Notification> messages)
            => ProcessRequest(() => _httpClient.PostAsJsonAsync($"{_rootUrl}/api/notification/customer/{customerId}", messages));

        private static async Task ProcessRequest(Func<Task<HttpResponseMessage>> request)
        {
            try
            {
                await request();
            }
            catch { }
        }
    }
}
