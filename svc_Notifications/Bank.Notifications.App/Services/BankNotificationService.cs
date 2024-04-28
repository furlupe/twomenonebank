using Bank.Common.Extensions;
using Bank.Notifications.App.Options;
using Bank.Notifications.App.Utils;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

namespace Bank.Notifications.App.Services
{
    public class BankNotificationService
    {
        private readonly FirebaseMessaging _messager;

        public BankNotificationService(IConfiguration configuration)
        {
            var fbOptions = configuration.GetConfigurationValue<FirebaseOptions>();
            var app = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "firebase.json")),
                ProjectId = fbOptions.ProjectId
            });

            _messager = FirebaseMessaging.GetMessaging(app);
        }

        public Task SubscribeClientToTopic(string topic, List<string> tokens)
            => _messager.SubscribeToTopicAsync(tokens, topic);

        public Task NotifyCustomer(string token, IEnumerable<Notification> messages)
            => _messager.SendEachAsync(messages.Select(m => new Message() { Notification = m, Token = token }));

        public  Task NotifyEmployees(IEnumerable<Notification> messages)
            => _messager.SendEachAsync(messages.Select(m => new Message() { Notification = m, Topic = Topics.Employee }));
    }
}
