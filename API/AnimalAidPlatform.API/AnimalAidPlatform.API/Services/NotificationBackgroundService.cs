
namespace AnimalAidPlatform.API.Services
{
    public class NotificationBackgroundService : BackgroundService
    {
        private readonly NotificationService _notificationService;
        private readonly ILogger<NotificationBackgroundService> _logger;

        public NotificationBackgroundService(
            NotificationService notificationService,
            ILogger<NotificationBackgroundService> logger)
        {
            _notificationService = notificationService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Notification Background Service is running.");

           // while (!stoppingToken.IsCancellationRequested)
           // {
                try
                {
                    // Üzenetek lekérdezése (polling) adatbázisból, queue-ból stb.
                    var messages = await PollMessagesAsync();

                    foreach (var message in messages)
                    {
                        // Értesítés küldése a NotificationService segítségével
                        await _notificationService.SendNotificationAsync(message);
                    }

                    // Várakozás, hogy ne terheljük túl a rendszert
                    await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Hiba történt a notification service-ben.");
                }
            //    }

            await Task.Delay(1000, stoppingToken); //Teszt
            _logger.LogInformation("Notification Background Service is stopping.");

        }

        private Task<List<string>> PollMessagesAsync()
        {
            // Itt kellene megvalósítani a valós üzenetek lekérdezését (pl. adatbázisból vagy queue-ból)
            return Task.FromResult(new List<string>
        {
            "asd"
        });
        }


    }
}
