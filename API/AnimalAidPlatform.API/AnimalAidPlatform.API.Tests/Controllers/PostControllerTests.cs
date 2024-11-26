using AnimalAidPlatform.API.Controllers;
using AnimalAidPlatform.API.Models.DTO.FeedPost;
using AnimalAidPlatform.API.Repositories.Interface;
using AnimalAidPlatform.API.Services;
using AnimalAidPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using AnimalAidPlatform.API.Services.Interface;
using AnimalAidPlatform.API.Models;
using AnimalAidPlatform.API.Models.DTO; // Make sure you import your interfaces

namespace AnimalAidPlatform.API.Tests.Controllers
{
    public class PostControllerTests
    {
        private readonly Mock<IFeedPostRepository> _mockFeedPostRepository;
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly Mock<INotificationStrategy> _mockNotificationStrategy;
        private readonly Mock<INotificationSettingsRepository> _mockNotificationSettingsRepository;
        private readonly Mock<INotificationRepository> _mockNotificationRepository;
        private readonly Mock<IFeedPostLikeRepository> _mockFeedPostLikeRepository;
        private readonly PostController _controller;

        public PostControllerTests()
        {
            _mockFeedPostRepository = new Mock<IFeedPostRepository>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _mockNotificationStrategy = new Mock<INotificationStrategy>();
            _mockNotificationSettingsRepository = new Mock<INotificationSettingsRepository>();
            _mockNotificationRepository = new Mock<INotificationRepository>();
            _mockFeedPostLikeRepository = new Mock<IFeedPostLikeRepository>();

            var notificationService = new NotificationService(
                _mockNotificationStrategy.Object,
                _mockNotificationSettingsRepository.Object,
                _mockNotificationRepository.Object
            );

            _controller = new PostController(
                _mockFeedPostRepository.Object,
                _mockUserManager.Object,
                _mockCategoryRepository.Object,
                notificationService,
                _mockFeedPostLikeRepository.Object
            );
        }

        private void SetupUserClaims(string userId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            };
            var identity = new ClaimsIdentity(claims, "mock");
            var principal = new ClaimsPrincipal(identity);
            _controller.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext
            {
                HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext() { User = principal }
            };
        }

        [Fact]
        public async Task GetFeedPosts_ShouldReturnOkResult_WhenPostsExist()
        {
            // Arrange
            var userId = "user123";
            SetupUserClaims(userId);

            // Mock the category data for the feed posts
            var category1 = new Category { Id = 1, Name = "Category 1", AssestIconHref = "asd", Urlhandle ="asd" };
            var category2 = new Category { Id = 2, Name = "Category 2" , AssestIconHref = "asd", Urlhandle = "asd" };

            // Set up the repository to return these categories when queried
            _mockCategoryRepository.Setup(repo => repo.GetCategoryById(1)).ReturnsAsync(category1);
            _mockCategoryRepository.Setup(repo => repo.GetCategoryById(2)).ReturnsAsync(category2);

            var feedPosts = new List<FeedPost>
{
    new FeedPost
    {
        Id = 1,
        Title = "Post 1",
        ContentText = "Content",
        CreatorId = userId,
        Creator = new ApplicationUser { Name = "Creator1" },  // Mock Creator object
        ImageUrl ="asdasd",
        CategoryId = 1,
        Category = category1
    },
    new FeedPost
    {
        Id = 2,
        Title = "Post 2",
        ContentText = "Content",
        CreatorId = userId,
        Creator = new ApplicationUser { Name = "Creator2" },  // Mock Creator object
        ImageUrl ="asdasd",
        CategoryId = 2,
        Category = category2
    }
};


            _mockFeedPostRepository.Setup(repo => repo.GetAllFeedPosts()).ReturnsAsync(feedPosts);
            _mockFeedPostLikeRepository.Setup(repo => repo.GetUserPostLikeAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync((FeedPostLike)null);

            // Act
            var result = await _controller.GetFeedPosts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var feedPostList = Assert.IsType<List<FeedPostResponseDTO>>(okResult.Value);

            Assert.Equal(2, feedPostList.Count);

            // Optionally, assert that categories are correctly included in the response
            Assert.Equal("Category 1", feedPostList[0].Category.Name);
            Assert.Equal("Category 2", feedPostList[1].Category.Name);
        }


        [Fact]
        public async Task PostFeedPost_ShouldReturnCreatedPostId_WhenValidRequest()
        {
            // Arrange
            var userId = "user123";
            SetupUserClaims(userId);

            var createPostDto = new CreatePostDTO
            {
                Title = "New Post",
                ContentText = "Content of new post",
                CategoryId = 1,
                Location = new LocationDTO { Latitude = 12.34, Longitude = 56.78, Address = "Test Address" }
            };

            var user = new ApplicationUser { Id = userId };
            var category = new Category { Id = 1, Name = "Category 1", AssestIconHref = "asd", Urlhandle = "asd" };

            // Initialize FeedPost with all required properties
            var createdPost = new FeedPost
            {
                Id = 1,
                Title = createPostDto.Title,
                CreatorId = userId,
                CategoryId = category.Id,
                Category = category,  // Make sure Category is set
                Address = createPostDto.Location.Address,  // Ensure Address is set
                GeoLat = createPostDto.Location.Latitude,  // Set latitude
                GeoLong = createPostDto.Location.Longitude,  // Set longitude
                LocUrl = "some_url"  // Ensure LocUrl is set if it's required
            };

            _mockUserManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
            _mockCategoryRepository.Setup(cr => cr.GetCategoryById(createPostDto.CategoryId)).ReturnsAsync(category);
            _mockFeedPostRepository.Setup(repo => repo.CreateFeedPost(It.IsAny<FeedPost>())).ReturnsAsync(createdPost);
            // Create a user to be returned by the mock
            var userToNotify = new ApplicationUser { Id = "user123", Name = "John Doe" };

            // Mock the repository call to return a list with this single user
            _mockNotificationSettingsRepository.Setup(repo => repo.GetUsersToNotifyAsync(null, createdPost.CategoryId))
                .ReturnsAsync(new List<ApplicationUser> { userToNotify });
            // Act
            var result = await _controller.PostFeedPost(createPostDto);

            // Assert
            var createdResult = Assert.IsType<OkObjectResult>(result.Result);
            var postId = Assert.IsType<int>(createdResult.Value);
            Assert.Equal(1, postId);
        }


        [Fact]
        public async Task LikePost_ShouldReturnUpdatedLikeCount_WhenPostIsLiked()
        {
            // Arrange
            var userId = "user123";
            SetupUserClaims(userId);

            var feedPostId = 1;
            var feedPost = new FeedPost { Id = feedPostId, Title = "Post 1", Likes = 0 };

            _mockFeedPostRepository.Setup(repo => repo.GetFeedPostById(feedPostId)).ReturnsAsync(feedPost);
            _mockFeedPostLikeRepository.Setup(repo => repo.GetUserPostLikeAsync(feedPostId, userId)).ReturnsAsync((FeedPostLike)null);
            _mockFeedPostRepository.Setup(repo => repo.LikePost(It.IsAny<FeedPost>(), true)).Callback<FeedPost, bool>((post, like) => post.Likes++);

            // Act
            var result = await _controller.LikePost(feedPostId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var likeCount = Assert.IsType<int>(okResult.Value);
            Assert.Equal(1, likeCount); // Like count should be 1 after liking the post
        }

        [Fact]
        public async Task DeleteFeedPost_ShouldReturnNoContent_WhenPostIsDeleted()
        {
            // Arrange
            var userId = "user123";
            SetupUserClaims(userId);

            var feedPostId = 1;
            var feedPost = new FeedPost { Id = feedPostId, CreatorId = userId };

            _mockFeedPostRepository.Setup(repo => repo.GetFeedPostById(feedPostId)).ReturnsAsync(feedPost);
            _mockFeedPostRepository.Setup(repo => repo.DeleteFeedPost(feedPostId)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteFeedPost(feedPostId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteFeedPost_ShouldReturnForbid_WhenUserIsNotOwner()
        {
            // Arrange
            var userId = "user123";
            SetupUserClaims(userId);

            var feedPostId = 1;
            var feedPost = new FeedPost { Id = feedPostId, CreatorId = "otherUserId" }; // Different creator

            _mockFeedPostRepository.Setup(repo => repo.GetFeedPostById(feedPostId)).ReturnsAsync(feedPost);

            // Act
            var result = await _controller.DeleteFeedPost(feedPostId);

            // Assert
            Assert.IsType<ForbidResult>(result);
        }
    }
}
