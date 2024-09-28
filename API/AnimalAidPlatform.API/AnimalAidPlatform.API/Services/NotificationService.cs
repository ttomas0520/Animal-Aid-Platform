using AnimalAidPlatform.API.Services.Interface;

namespace AnimalAidPlatform.API.Services
{
    public class NotificationService
    {
        private readonly INotificationStrategy _notificationStrategy;

        public NotificationService(INotificationStrategy notificationStrategy)
        {
            _notificationStrategy = notificationStrategy;
        }

        public async Task SendNotificationAsync(string message)
        {
            // A megfelelő stratégia használata
            await _notificationStrategy.SendNotificationAsync(message);
        }
    }
}
