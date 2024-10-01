using AnimalAidPlatform.API.Repositories.Interface;

namespace AnimalAidPlatform.API.Services
{
    public class NotificationBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<NotificationBackgroundService> _logger;

        public NotificationBackgroundService(
            IServiceProvider serviceProvider,
            ILogger<NotificationBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Notification Background Service is running.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope()) 
                    {
                        var notificationService = scope.ServiceProvider.GetRequiredService<NotificationService>();
                        var notificationRepository = scope.ServiceProvider.GetRequiredService<INotificationRepository>();

                        var notifications = await notificationRepository.GetNotificationsAsync(100);

                        foreach (var notification in notifications)
                        {
                            await notificationService.SendNotificationAsync(notification);
                            await notificationRepository.DeleteNotificationAsync(notification.Id);
                        }
                    }
                    await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Hiba történt a notification service-ben.");
                }
            }

            _logger.LogInformation("Notification Background Service is stopping.");
        }
    }


}
