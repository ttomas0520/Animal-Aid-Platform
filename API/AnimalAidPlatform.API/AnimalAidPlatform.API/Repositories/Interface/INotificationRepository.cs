using AnimalAidPlatform.API.Models;

namespace AnimalAidPlatform.API.Repositories.Interface
{
    public interface INotificationRepository
    {
        Task<List<Notification>> GetNotificationsAsync(int maxCount);
        Task DeleteNotificationAsync(int notificationId);
        Task CreateNotificationsAsync(IEnumerable<Notification> notifications);
    }
}
