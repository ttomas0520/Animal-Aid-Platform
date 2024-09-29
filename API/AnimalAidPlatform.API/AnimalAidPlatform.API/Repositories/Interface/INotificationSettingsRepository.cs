using AnimalAidPlatform.API.Models;

namespace AnimalAidPlatform.API.Repositories.Interface
{
    public interface INotificationSettingsRepository
    {
        NotificationSettings GetByUserId(string userId);
        void Upsert(NotificationSettings notificationSettings);
        Task<List<NotificationSettings>> GetUsersToNotifyAsync(double latitude, double longitude, int categoryId);
    }
}
