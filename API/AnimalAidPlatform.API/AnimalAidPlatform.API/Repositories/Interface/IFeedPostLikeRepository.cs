using AnimalAidPlatform.API.Models;

namespace AnimalAidPlatform.API.Repositories.Interface
{
    public interface IFeedPostLikeRepository
    {
        Task<FeedPostLike?> GetUserPostLikeAsync(int feedPostId, string userId);
        Task AddLikeAsync(FeedPostLike postLike);
        Task RemoveLikeAsync(int feedPostId, string userId);

    }
}
