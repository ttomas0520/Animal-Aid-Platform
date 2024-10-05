using AnimalAidPlatform.API.Data;
using AnimalAidPlatform.API.Models;
using AnimalAidPlatform.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace AnimalAidPlatform.API.Repositories.Implementation
{
    public class FeedPostLikeRepository: IFeedPostLikeRepository
    {
        private readonly ApplicationDbContext _context;

        public FeedPostLikeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FeedPostLike?> GetUserPostLikeAsync(int feedPostId, string userId)
        {
            return await _context.FeedPostLikes
                                 .FirstOrDefaultAsync(pl => pl.FeedPostId == feedPostId && pl.UserId == userId);
        }

        public async Task AddLikeAsync(FeedPostLike postLike)
        {
            await _context.FeedPostLikes.AddAsync(postLike);
            await _context.SaveChangesAsync();
        }

        // New method to remove a like
        public async Task RemoveLikeAsync(int feedPostId, string userId)
        {
            var like = await GetUserPostLikeAsync(feedPostId, userId);
            if (like != null)
            {
                _context.FeedPostLikes.Remove(like);
                await _context.SaveChangesAsync();
            }
        }
    }
}
