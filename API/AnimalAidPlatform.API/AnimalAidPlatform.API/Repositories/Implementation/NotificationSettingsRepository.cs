using AnimalAidPlatform.API.Data;
using AnimalAidPlatform.API.Models;
using AnimalAidPlatform.API.Repositories.Interface;
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
    }
}
