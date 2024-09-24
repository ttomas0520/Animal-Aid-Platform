using AnimalAidPlatform.API.Models;
using AnimalAidPlatform.Models;

namespace AnimalAidPlatform.API.Repositories.Interface
{
    public interface IFeedPostRepository
    {
        Task<IEnumerable<FeedPost>> GetAllFeedPosts();
        Task<FeedPost> GetFeedPostById(int id);
        Task<FeedPost> CreateFeedPost(FeedPost feedPost);
        Task<FeedPost> UpdateFeedPost(int id, FeedPost feedPost);
        Task<bool> DeleteFeedPost(int id);
        Task<IEnumerable<FeedPost>> GetAllFeedPostCreatedByUser(string userId);
    }
}
