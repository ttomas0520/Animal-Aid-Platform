using AnimalAidPlatform.API.Models;
using AnimalAidPlatform.API.Models.DTO.FeedPost;
using AnimalAidPlatform.API.Repositories.Interface;
using AnimalAidPlatform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Spatial;

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
        public PostController(IFeedPostRepository feedPostRepository, UserManager<ApplicationUser> userManager, ICategoryRepository categoryRepository)
        {
            _feedPostRepository = feedPostRepository;
            _userManager = userManager;
            _categoryRepository = categoryRepository;
        }

        // GET: api/FeedPosts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedPostResponseDTO>>> GetFeedPosts()
        {
            var feedPosts = await _feedPostRepository.GetAllFeedPosts();
            var resp = new List<FeedPostResponseDTO>();
            foreach (var feedPost in feedPosts)
            {
                resp.Add(new FeedPostResponseDTO
                {
                    Id = feedPost.Id,
                    Title = feedPost.Title,
                    ContentText = feedPost.ContentText,
                    UserID = feedPost.CreatorId,
                    Location = new Models.DTO.LocationDTO { Address = feedPost.Address, Latitude = feedPost.GeoLat, Longitude = feedPost.GeoLong },
                    Category = new Models.DTO.Category.CategoryDto { Id = feedPost.CategoryId, Name = feedPost.Category.Name, Urlhandle = feedPost.Category.Urlhandle }
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
            var createPost = new FeedPost()
            {
                Title = request.Title,
                ContentText = request.ContentText,
                GeoLong = request.Location.Longitude,
                GeoLat = request.Location.Latitude,
                Address = request.Location.Address,
                PostDate = DateTime.Now,
                Creator = user,
                Category = category,

            };
            var createdFeedPost = await _feedPostRepository.CreateFeedPost(createPost);
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

    }
}
