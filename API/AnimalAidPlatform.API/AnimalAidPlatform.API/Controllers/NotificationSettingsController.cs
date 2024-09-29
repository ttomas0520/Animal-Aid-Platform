using AnimalAidPlatform.API.Data;
using AnimalAidPlatform.API.Models;
using AnimalAidPlatform.API.Models.DTO;
using AnimalAidPlatform.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using System.Security.Claims;

namespace AnimalAidPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationSettingsController : ControllerBase
    {
        private readonly INotificationSettingsRepository _repository;
        private readonly ApplicationDbContext _context;

        public NotificationSettingsController(INotificationSettingsRepository repository, ApplicationDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        // GET: api/NotificationSettings/
        [HttpGet]
        public ActionResult<NotificationSettingsDto> Get()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var notificationSettings = _repository.GetByUserId(currentUserId);
            if (notificationSettings == null)
            {
                return NotFound("Notification settings not found.");
            }

            var notificationSettingsDto = new NotificationSettingsDto
            {
                PushNotificationEnabled = notificationSettings.PushNotificationEnabled,
                Radius = notificationSettings.Radius,
                Location = new LocationDTO
                {
                    Latitude = notificationSettings.GeoLat,
                    Longitude = notificationSettings.GeoLong,
                    Address = notificationSettings.Address
                },
                CategoryIds = notificationSettings.Categories.Select(c => c.Id).ToList()
            };

            return Ok(notificationSettingsDto);
        }

        // PUT: api/NotificationSettings
        [HttpPut]
        public IActionResult Upsert([FromBody] NotificationSettingsDto dto)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.Find(currentUserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var newCategories = _context.Categories.Where(c => dto.CategoryIds.Contains(c.Id)).ToList();

            var notificationSettings = new NotificationSettings
            {
                UserId = currentUserId,
                User = user,
                PushNotificationEnabled = dto.PushNotificationEnabled,
                GeoLat = dto.Location.Latitude,
                GeoLong = dto.Location.Longitude,
                Address = dto.Location.Address,
                Radius = dto.Radius,
                Categories = newCategories,
                Location = (dto.Location.Latitude != 0 && dto.Location.Longitude != 0)
                    ? new Point(dto.Location.Longitude, dto.Location.Latitude)
                    : null 
            };

            _repository.Upsert(notificationSettings);

            return Ok("Notification settings have been saved.");
        }
    }

}
