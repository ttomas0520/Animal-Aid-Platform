using AnimalAidPlatform.API.Models;
using AnimalAidPlatform.API.Repositories.Interface;
using AnimalAidPlatform.API.Services.Interface;

namespace AnimalAidPlatform.API.Services
{
    public class NotificationService
    {
        private readonly INotificationStrategy _notificationStrategy;
        private readonly INotificationSettingsRepository _notificationSettingsRepository;
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationStrategy notificationStrategy, INotificationSettingsRepository notificationSettingsRepository,
                               INotificationRepository notificationRepository)
        {
            _notificationSettingsRepository = notificationSettingsRepository;
            _notificationRepository = notificationRepository;
            _notificationStrategy = notificationStrategy;
        }

        public async Task SendNotificationAsync(Notification notification)
        {
            await _notificationStrategy.SendNotificationAsync(notification);
        }

        public async Task CreateNotificationsForFeedPost(FeedPost feedPost)
        {
            var usersToNotify = await _notificationSettingsRepository.GetUsersToNotifyAsync(feedPost.GeoLat, feedPost.GeoLong, feedPost.CategoryId);

            var notifications = new List<Notification>();

            foreach (var userSetting in usersToNotify)
            {
                var notification = new Notification
                {
                    UserId = userSetting.UserId,
                    Message = $"Új poszt: {feedPost.Title}\n{feedPost.ContentText}"
                };

                notifications.Add(notification);
            }

            await _notificationRepository.CreateNotificationsAsync(notifications);
        }

    }
}
