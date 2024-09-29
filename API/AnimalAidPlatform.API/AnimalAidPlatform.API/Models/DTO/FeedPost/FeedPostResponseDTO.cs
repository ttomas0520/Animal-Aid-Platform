using AnimalAidPlatform.API.Models.DTO.Category;

namespace AnimalAidPlatform.API.Models.DTO.FeedPost
{
    public class FeedPostResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ContentText { get; set; }
        public LocationDTO Location { get; set; }
        public CategoryDto Category { get; set; }
        public string UserID { get; set; }
        public string CreatorName { get; set; }
        public string ImageUrl { get; set; }
    }
}
