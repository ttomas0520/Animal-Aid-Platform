using AnimalAidPlatform.API.Models;
using AnimalAidPlatform.Models;
using NetTopologySuite.Geometries;

namespace AnimalAidPlatform.API.Repositories.Interface
{
    public interface INotificationSettingsRepository
    {
        NotificationSettings GetByUserId(string userId);
        void Upsert(NotificationSettings notificationSettings);
        Task<List<ApplicationUser>> GetUsersToNotifyAsync(Point postLocation, int categoryId);
    }
}
