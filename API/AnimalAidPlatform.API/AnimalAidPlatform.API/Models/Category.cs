using AnimalAidPlatform.API.Models.Enums;

namespace AnimalAidPlatform.API.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Urlhandle { get; set; }
    }
}
