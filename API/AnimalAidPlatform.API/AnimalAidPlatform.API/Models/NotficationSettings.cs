using AnimalAidPlatform.Models;
using NetTopologySuite.Geometries;

namespace AnimalAidPlatform.API.Models
{
    public class NotificationSettings
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public bool PushNotificationEnabled { get; set; }

        public double GeoLong { get; set; }
        public double GeoLat { get; set; }
        public string Address { get; set; }
        public Point Location { get; set; }


        public double Radius { get; set; }

        // Many-to-many relationship with Category
        public ICollection<Category> Categories { get; set; }
    }

}
