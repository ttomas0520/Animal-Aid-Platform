using AnimalAidPlatform.API.Controllers;
using AnimalAidPlatform.API.Models.DTO.User;
using AnimalAidPlatform.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AnimalAidPlatform.API.Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly Mock<RoleManager<IdentityRole>> _mockRoleManager;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null
            );
            _mockRoleManager = new Mock<RoleManager<IdentityRole>>(
                Mock.Of<IRoleStore<IdentityRole>>(), null, null, null, null
            );

            var inMemorySettings = new Dictionary<string, string>
            {
                {"JWTSettings:securityKey", "ThisIsASecretKeyThisIsASecretKeyThisIsASecretKey"},
                {"JWTSettings:validAudience", "ValidAudience"},
                {"JWTSettings:validIssuer", "ValidIssuer"}
            };

            // Create an IConfiguration instance using the in-memory settings
            var _mockConfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _controller = new UserController(_mockUserManager.Object, _mockRoleManager.Object, _mockConfiguration);
        }

        [Fact]
        public async Task Register_ShouldReturnOk_WhenUserIsRegistered()
        {
            // Arrange
            var registerDto = new RegisterDTO
            {
                Email = "test@example.com",
                Password = "Password123!",
                Name = "Test User",
                PhoneNumber = "123456789",
                Roles = new List<string>() {"ADMIN"}
            };

            var user = new ApplicationUser { Email = registerDto.Email, UserName = registerDto.Email, PhoneNumber = registerDto.PhoneNumber };
            _mockUserManager.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _mockUserManager.Setup(um => um.AddToRolesAsync(It.IsAny<ApplicationUser>(), It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.Register(registerDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<AuthResponseDTO>(okResult.Value);
            Assert.True(response.IsSucces);
            Assert.Equal("Fiók létrehozása sikeres", response.Message);
        }

        [Fact]
        public async Task Login_ShouldReturnToken_WhenCredentialsAreCorrect()
        {
            // Arrange
            var loginDto = new LoginDTO { Email = "test@example.com", Password = "Password123!" };
            var user = new ApplicationUser { Email = loginDto.Email, UserName = loginDto.Email };

            // Mock UserManager behavior
            _mockUserManager.Setup(um => um.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            _mockUserManager.Setup(um => um.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(true);

            // Mock the configuration for JWT settings
            var mockJwtSettings = new Mock<IConfigurationSection>();
            mockJwtSettings.Setup(s => s["securityKey"]).Returns("ThisIsASecretKey");
            mockJwtSettings.Setup(s => s["validAudience"]).Returns("ValidAudience");
            mockJwtSettings.Setup(s => s["validIssuer"]).Returns("ValidIssuer");

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(c => c.GetSection("JWTSettings")).Returns(mockJwtSettings.Object);

            _mockUserManager.Setup(um => um.GetRolesAsync(It.IsAny<ApplicationUser>()))
                    .ReturnsAsync(new List<string> { "ADMIN" }); 

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<AuthResponseDTO>(okResult.Value);
            Assert.True(response.IsSucces);
            Assert.NotNull(response.Token);
        }




        [Fact]
        public async Task GetUserDetail_ShouldReturnUserDetails_WhenUserIsAuthenticated()
        {
            // Arrange
            var currentUserId = "user-id-123";
            var user = new ApplicationUser { Id = currentUserId, Email = "test@example.com", Name = "test@example.com" };
            _mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
            _mockUserManager.Setup(um => um.GetRolesAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(new List<string> { "REGULAR" });

            // Simulate the authenticated user
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, currentUserId)
                    }))
                }
            };

            // Act
            var result = await _controller.GetUserDetail();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var userDetail = Assert.IsType<UserDetailDTO>(okResult.Value);
            Assert.Equal("test@example.com", userDetail.Email);
            Assert.Equal("test@example.com", userDetail.Name);
            Assert.Single(userDetail.Roles);
            Assert.Contains("REGULAR", userDetail.Roles);
        }
    }
}
