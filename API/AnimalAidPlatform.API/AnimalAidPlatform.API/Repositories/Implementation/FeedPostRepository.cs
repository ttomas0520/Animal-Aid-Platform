using AnimalAidPlatform.API.Data;
using AnimalAidPlatform.API.Models;
using AnimalAidPlatform.API.Repositories.Interface;
using AnimalAidPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimalAidPlatform.API.Repositories.Implementation
{
    public class FeedPostRepository : IFeedPostRepository
    {
        private readonly ApplicationDbContext _context;

        public FeedPostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FeedPost>> GetAllFeedPosts()
        {
            return await _context.FeedPosts.ToListAsync();
        }

        public async Task<FeedPost> GetFeedPostById(int id)
        {
            return await _context.FeedPosts.FirstOrDefaultAsync(fp => fp.Id == id);
        }

        public async Task<FeedPost> CreateFeedPost(FeedPost feedPost)
        {
            _context.FeedPosts.Add(feedPost);
            await _context.SaveChangesAsync();
            return feedPost;
        }

        public async Task<FeedPost> UpdateFeedPost(int id, FeedPost feedPost)
        {
            var existingFeedPost = await _context.FeedPosts.FirstOrDefaultAsync(fp => fp.Id == id);
            if (existingFeedPost != null)
            {
                existingFeedPost.Title = feedPost.Title;
                existingFeedPost.ContentText = feedPost.ContentText;
                existingFeedPost.ImageUrl = feedPost.ImageUrl;
                existingFeedPost.UrlHandle = feedPost.UrlHandle;
                existingFeedPost.PostDate = feedPost.PostDate;
                existingFeedPost.Creator = feedPost.Creator;
                existingFeedPost.AssociatedAnimals = feedPost.AssociatedAnimals;

                await _context.SaveChangesAsync();
            }
            return existingFeedPost;
        }

        public async Task<bool> DeleteFeedPost(int id)
        {
            var feedPostToDelete = await _context.FeedPosts.FirstOrDefaultAsync(fp => fp.Id == id);
            if (feedPostToDelete != null)
            {
                _context.FeedPosts.Remove(feedPostToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
