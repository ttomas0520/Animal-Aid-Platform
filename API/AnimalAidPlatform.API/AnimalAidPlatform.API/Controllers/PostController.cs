using AnimalAidPlatform.API.Models;
using AnimalAidPlatform.API.Models.DTO.FeedPost;
using AnimalAidPlatform.API.Repositories.Implementation;
using AnimalAidPlatform.API.Repositories.Interface;
using AnimalAidPlatform.API.Services;
using AnimalAidPlatform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System.Security.Claims;

namespace AnimalAidPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly IFeedPostRepository _feedPostRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICategoryRepository _categoryRepository;
        private readonly NotificationService _notificationService;
        private readonly IFeedPostLikeRepository _feedPostLikeRepository;
        public PostController(IFeedPostRepository feedPostRepository, UserManager<ApplicationUser> userManager, ICategoryRepository categoryRepository, NotificationService notificationService, IFeedPostLikeRepository feedPostLikeRepository)
        {
            _feedPostRepository = feedPostRepository;
            _userManager = userManager;
            _categoryRepository = categoryRepository;
            _notificationService = notificationService;
            _feedPostLikeRepository = feedPostLikeRepository;
        }

        // GET: api/FeedPosts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedPostResponseDTO>>> GetFeedPosts()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var feedPosts = await _feedPostRepository.GetAllFeedPosts();
            var resp = new List<FeedPostResponseDTO>();
            foreach (var feedPost in feedPosts)
            {
                bool isItLiked = (await _feedPostLikeRepository.GetUserPostLikeAsync(feedPost.Id, currentUserId)) != null;
                resp.Add(new FeedPostResponseDTO
                {
                    Id = feedPost.Id,
                    Title = feedPost.Title,
                    ContentText = feedPost.ContentText,
                    UserID = feedPost.CreatorId,
                    CreatorName = feedPost.Creator.Name,
                    ImageUrl = feedPost.ImageUrl,
                    Location = new Models.DTO.LocationDTO { Address = feedPost.Address, Latitude = feedPost.GeoLat, Longitude = feedPost.GeoLong },
                    Category = new Models.DTO.Category.CategoryDto { Id = feedPost.CategoryId, Name = feedPost.Category.Name, Urlhandle = feedPost.Category.Urlhandle },
                    LikeNumber = feedPost.Likes,
                    IsLiked = isItLiked,

                }); 
            }
            return Ok(resp);
        }

        // GET: api/FeedPosts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FeedPost>> GetFeedPost(int id)
        {
            var feedPost = await _feedPostRepository.GetFeedPostById(id);

            if (feedPost == null)
            {
                return NotFound();
            }

            return feedPost;
        }

        // POST: api/FeedPosts
        [HttpPost]
        public async Task<ActionResult<int>> PostFeedPost(CreatePostDTO request)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(currentUserId!);
            var category = await _categoryRepository.GetCategoryById(request.CategoryId);
            var point = new Point(request.Location.Longitude, request.Location.Latitude);
            point.SRID = 4326;
            var createPost = new FeedPost()
            {
                Title = request.Title,
                ContentText = request.ContentText,
                GeoLong = request.Location.Longitude,
                GeoLat = request.Location.Latitude,
                Address = request.Location.Address,
                ImageUrl = request.ImageUrl,
                PostDate = DateTime.Now,
                Creator = user,
                Category = category,
                Location = point,

            };
            var createdFeedPost = await _feedPostRepository.CreateFeedPost(createPost);
            await this._notificationService.CreateNotificationsForFeedPost(createdFeedPost);
            return Ok(createdFeedPost.Id);
        }

        // PUT: api/FeedPosts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedPost(int id, FeedPost feedPost)
        {
            if (id != feedPost.Id)
            {
                return BadRequest();
            }

            var updatedFeedPost = await _feedPostRepository.UpdateFeedPost(id, feedPost);

            if (updatedFeedPost == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/FeedPosts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedPost(int id)
        {
            var result = await _feedPostRepository.DeleteFeedPost(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<FeedPostResponseDTO>>> GetAllFeedPostsCreatedByUser(string userId)
        {
            var feedPosts = await _feedPostRepository.GetAllFeedPostCreatedByUser(userId);
            var resp = new List<FeedPostResponseDTO>();
            foreach (var feedPost in feedPosts)
            {
                bool isItLiked = (await _feedPostLikeRepository.GetUserPostLikeAsync(feedPost.Id, userId)) != null;
                resp.Add(new FeedPostResponseDTO
                {
                    Id = feedPost.Id,
                    Title = feedPost.Title,
                    ContentText = feedPost.ContentText,
                    UserID = feedPost.CreatorId,
                    CreatorName = feedPost.Creator.Name,
                    ImageUrl = feedPost.ImageUrl,
                    Location = new Models.DTO.LocationDTO { Address = feedPost.Address, Latitude = feedPost.GeoLat, Longitude = feedPost.GeoLong },
                    Category = new Models.DTO.Category.CategoryDto { Id = feedPost.CategoryId, Name = feedPost.Category.Name, Urlhandle = feedPost.Category.Urlhandle },
                    LikeNumber = feedPost.Likes,
                    IsLiked = isItLiked,
                });
            }
            return Ok(resp);
        }

        [HttpPost("{id}/like")]
        public async Task<ActionResult<int>> LikePost(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get current user ID
            var post = await _feedPostRepository.GetFeedPostById(id);
            if (post == null) return NotFound();

            var existingLike = await _feedPostLikeRepository.GetUserPostLikeAsync(id, userId);

            if (existingLike != null)
            {
                // If the like exists, remove it (user is "unliking" the post)
                await _feedPostLikeRepository.RemoveLikeAsync(id, userId);
                await _feedPostRepository.LikePost(post, false);
            }
            else
            {
                // If no like exists, add a new like
                var postLike = new FeedPostLike
                {
                    FeedPostId = id,
                    UserId = userId,
                };
                await _feedPostLikeRepository.AddLikeAsync(postLike);
                await _feedPostRepository.LikePost(post, true);
            }

            return Ok(post.Likes);
        }


    }
}
