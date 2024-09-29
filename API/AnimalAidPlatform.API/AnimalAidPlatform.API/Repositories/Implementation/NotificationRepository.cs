using AnimalAidPlatform.API.Data;
using AnimalAidPlatform.API.Models;
using AnimalAidPlatform.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace AnimalAidPlatform.API.Repositories.Implementation
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Notification>> GetNotificationsAsync(int maxCount)
        {
            return await _context.Notifications
                .OrderBy(n => n.CreatedAt)
                .Take(maxCount)
                .ToListAsync();
        }

        public async Task DeleteNotificationAsync(int notificationId)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification != null)
            {
                _context.Notifications.Remove(notification);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CreateNotificationsAsync(IEnumerable<Notification> notifications)
        {
            await _context.Notifications.AddRangeAsync(notifications);
            await _context.SaveChangesAsync();
        }
    }

}
