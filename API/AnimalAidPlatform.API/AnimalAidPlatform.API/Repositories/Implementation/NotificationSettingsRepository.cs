using AnimalAidPlatform.API.Data;
using AnimalAidPlatform.API.Models;
using AnimalAidPlatform.API.Repositories.Interface;
using AnimalAidPlatform.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System.Globalization;

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
                existingSettings.Location = notificationSettings.Location;

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

        public async Task<List<ApplicationUser>> GetUsersToNotifyAsync(Point postLocation, int categoryId)
        {
            var latitude = postLocation.Y.ToString(CultureInfo.InvariantCulture);
            var longitude = postLocation.X.ToString(CultureInfo.InvariantCulture);

            Console.WriteLine($"Latitude: {latitude}, Longitude: {longitude}, CategoryId: {categoryId}");

            var resp = await _context.Users.FromSqlRaw(@"
        SELECT u.*
        FROM NotificationSettings ns
        INNER JOIN CategoryNotificationSettings cns ON cns.NotificationSettingsId = ns.Id
        INNER JOIN AspNetUsers u ON ns.UserId = u.Id
        WHERE cns.CategoriesId = @CategoryId
        AND ns.PushNotificationEnabled = 1
        AND ns.Location IS NOT NULL
        AND ns.Location.STDistance(geography::Point(@Lat, @Long, 4326)) <= ns.Radius * 1000",
                new SqlParameter("@Lat", latitude),
                new SqlParameter("@Long", longitude),
                new SqlParameter("@CategoryId", categoryId))
                .AsSplitQuery()
                .ToListAsync();
            return resp;
        }




    }
}
