using AnimalAidPlatform.API.Data;
using AnimalAidPlatform.API.Models;
using AnimalAidPlatform.API.Repositories.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AnimalAidPlatform.API.Repositories.Implementation
{
    public class NotificationSettingsRepository : INotificationSettingsRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationSettingsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public NotificationSettings GetByUserId(string userId)
        {
            return _context.NotificationSettings
                .Include(ns => ns.Categories)  // Eager loading a kategóriákhoz
                .FirstOrDefault(ns => ns.UserId == userId);
        }

        public void Upsert(NotificationSettings notificationSettings)
        {
            var existingSettings = GetByUserId(notificationSettings.UserId);

            if (existingSettings == null)
            {
                // Létrehozzuk, ha nem létezik
                _context.NotificationSettings.Add(notificationSettings);
            }
            else
            {
                // Frissítjük, ha létezik
                existingSettings.PushNotificationEnabled = notificationSettings.PushNotificationEnabled;
                existingSettings.GeoLat = notificationSettings.GeoLat;
                existingSettings.GeoLong = notificationSettings.GeoLong;
                existingSettings.Address = notificationSettings.Address;
                existingSettings.Radius = notificationSettings.Radius;

                // Kategóriák frissítése
                UpdateCategories(existingSettings, notificationSettings.Categories);

                _context.NotificationSettings.Update(existingSettings);
            }

            _context.SaveChanges();
        }

        private void UpdateCategories(NotificationSettings existingSettings, ICollection<Category> newCategories)
        {
            var existingCategoryIds = existingSettings.Categories.Select(c => c.Id).ToList();
            var newCategoryIds = newCategories.Select(c => c.Id).ToList();

            // Hozzáadjuk az új kategóriákat
            foreach (var category in newCategories.Where(c => !existingCategoryIds.Contains(c.Id)))
            {
                existingSettings.Categories.Add(category);
            }

            // Eltávolítjuk a már nem kívánt kategóriákat
            foreach (var category in existingSettings.Categories.Where(c => !newCategoryIds.Contains(c.Id)).ToList())
            {
                existingSettings.Categories.Remove(category);
            }
        }

        public async Task<List<NotificationSettings>> GetUsersToNotifyAsync(double latitude, double longitude, int categoryId)
        {
            return await _context.NotificationSettings.FromSqlRaw(@"
                    SELECT ns.*
                    FROM NotificationSettings ns
                    CROSS APPLY (SELECT geography::Point(@Lat, @Long, 4326) AS PostLocation) AS pl
                    WHERE ns.Categories.Any(c => c.Id == @CategoryId)
                    AND ns.Location IS NOT NULL
                    AND ns.Location.STDistance(pl.PostLocation) <= ns.Radius * 1000",
                    new SqlParameter("@Lat", latitude),
                    new SqlParameter("@Long", longitude),
                    new SqlParameter("@CategoryId", categoryId))
                    .Include(ns => ns.User)
                    .Include(ns => ns.Categories)
                    .ToListAsync();
        }
    }
}
