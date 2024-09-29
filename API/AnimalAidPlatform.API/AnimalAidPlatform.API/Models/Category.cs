namespace AnimalAidPlatform.API.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Urlhandle { get; set; }
        public ICollection<FeedPost> Posts { get; set; }
        public ICollection<NotificationSettings> NotificationSettings { get; set; }
    }
}
