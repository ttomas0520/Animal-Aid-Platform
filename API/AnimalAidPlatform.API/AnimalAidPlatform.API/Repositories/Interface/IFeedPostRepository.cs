using AnimalAidPlatform.Models;

namespace AnimalAidPlatform.API.Repositories.Interface
{
    public interface IFeedPostRepository
    {
        Task<IEnumerable<FeedPost>> GetAllFeedPosts();
        Task<FeedPost> GetFeedPostById(Guid id);
        Task<FeedPost> CreateFeedPost(FeedPost feedPost);
        Task<FeedPost> UpdateFeedPost(Guid id, FeedPost feedPost);
        Task<bool> DeleteFeedPost(Guid id);
    }
}
