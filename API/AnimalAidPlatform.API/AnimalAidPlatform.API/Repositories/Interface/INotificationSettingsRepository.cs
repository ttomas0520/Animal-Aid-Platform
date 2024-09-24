using AnimalAidPlatform.API.Models;

namespace AnimalAidPlatform.API.Repositories.Interface
{
    public interface INotificationSettingsRepository
    {
        NotificationSettings GetByUserId(string userId);
        void Upsert(NotificationSettings notificationSettings);
    }
}
