using AnimalAidPlatform.Models;

namespace AnimalAidPlatform.API.Models
{
    public class FeedPostLike
    {
        public int FeedPostId { get; set; }
        public FeedPost FeedPost { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime LikedAt { get; set; } = DateTime.Now;
    }
}
