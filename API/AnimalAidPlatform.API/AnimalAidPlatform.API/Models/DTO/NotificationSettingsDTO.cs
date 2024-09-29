namespace AnimalAidPlatform.API.Models.DTO
{
    public class NotificationSettingsDto
    {
        public bool PushNotificationEnabled { get; set; }
        public LocationDTO Location { get; set; }  // LocationDTO használata
        public double Radius { get; set; }
        public List<int> CategoryIds { get; set; }  // Csak az ID-ket fogadjuk a kategóriákhoz
    }

}
