using AnimalAidPlatform.API.Controllers;
using AnimalAidPlatform.API.Models.DTO;
using AnimalAidPlatform.API.Repositories.Interface;
using AnimalAidPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Drawing;
using Xunit;
using Assert = Xunit.Assert;
using NetTopologySuite.Geometries;
using Point = NetTopologySuite.Geometries.Point;

namespace AnimalAidPlatform.API.Tests.Controllers
{
    public class AnimalShelterControllerTests
    {
        private readonly Mock<IAnimalShelterRepository> _mockRepository;
        private readonly AnimalShelterController _controller;

        public AnimalShelterControllerTests()
        {
            _mockRepository = new Mock<IAnimalShelterRepository>();
            _controller = new AnimalShelterController(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfShelters()
        {
            // Arrange
            var mockShelters = new List<AnimalShelter>
            {
                new AnimalShelter { Id = 1, Name = "Shelter 1" },
                new AnimalShelter { Id = 2, Name = "Shelter 2" }
            };
            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(mockShelters);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Xunit.Assert.IsType<OkObjectResult>(result.Result);
            var returnedShelters = Assert.IsType<List<AnimalShelterDTO>>(okResult.Value);
            Assert.Equal(2, returnedShelters.Count);
        }

        [Fact]
        public async Task GetById_ShouldReturnShelter_WhenExists()
        {
            // Arrange
            var shelter = new AnimalShelter { Id = 1, Name = "Shelter 1" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(shelter);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedShelter = Assert.IsType<AnimalShelterDTO>(okResult.Value);
            Assert.Equal(1, returnedShelter.Id);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((AnimalShelter)null);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Add_ShouldReturnCreatedResult_WhenModelIsValid()
        {
            // Arrange
            var validShelterDto = new AnimalShelterDTO
            {
                Id = 1,
                Name = "Test Shelter",
                Type = "General",
                PhoneNumber = "+36123456789",
                Email = "shelter@test.com",
                ContactName = "Test Contact",
                ContactPosition = "Manager",
                Location = new LocationDTO
                {
                    Longitude = 19.0402,
                    Latitude = 47.4979,
                    Address = "Budapest, Hungary",
                    Url = "http://test-location-url.com"
                },
                Adoption = true,
                Visiting = false,
                Volunteering = true,
                MedicalCare = false,
                Donations = true
            };

            var mockRepository = new Mock<IAnimalShelterRepository>();
            mockRepository.Setup(repo => repo.AddAsync(It.IsAny<AnimalShelter>()))
                          .Returns(Task.CompletedTask);

            var controller = new AnimalShelterController(mockRepository.Object);

            // Act
            var result = await controller.Add(validShelterDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetById", createdResult.ActionName);
            Assert.IsType<AnimalShelterDTO>(createdResult.Value);
        }


        [Fact]
        public async Task Update_ShouldReturnOk_WhenShelterExists()
        {
            // Arrange
            var shelter = new AnimalShelter
            {
                Id = 1,
                Name = "Shelter 1",
                GeoLong = 19.0402,
                GeoLat = 47.4979,
                Address = "Budapest, Hungary",
                PhoneNumber = "+36123456789",
                Email = "shelter1@test.com",
                ContactName = "Test Contact",
                ContactPosition = "Manager",
                Location = new NetTopologySuite.Geometries.Point(19.0402, 47.4979) { SRID = 4326 }
            };

            var shelterDto = new AnimalShelterDTO
            {
                Id = 1,
                Name = "Updated Shelter",
                Type = "General",
                PhoneNumber = "+36123456789",
                Email = "updated@test.com",
                ContactName = "Updated Contact",
                ContactPosition = "Manager",
                Location = new LocationDTO
                {
                    Longitude = 19.0402,
                    Latitude = 47.4979,
                    Address = "Updated Address",
                    Url = "http://updated-location-url.com"
                },
                Adoption = true,
                Visiting = false,
                Volunteering = true,
                MedicalCare = false,
                Donations = true
            };

            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(shelter);

            // Ensure UpdateAsync returns the updated shelter
            var updatedShelter = new AnimalShelter
            {
                Id = 1,
                Name = "Updated Shelter",  // Simulating the update
                GeoLong = 19.0402,
                GeoLat = 47.4979,
                Address = "Updated Address",
                PhoneNumber = "+36123456789",
                Email = "updated@test.com",
                ContactName = "Updated Contact",
                ContactPosition = "Manager",
                Location = new NetTopologySuite.Geometries.Point(19.0402, 47.4979) { SRID = 4326 }
            };

            _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<AnimalShelter>())).ReturnsAsync(updatedShelter);

            // Act
            var result = await _controller.Update(1, shelterDto);

            // Assert
            var okResult = Assert.IsType< OkObjectResult > (result.Result);  // Update expected type
            var returnedShelter = Assert.IsType<AnimalShelterDTO>(okResult.Value);
            Assert.Equal("Updated Shelter", returnedShelter.Name);
        }





        [Fact]
        public async Task Update_ShouldReturnNotFound_WhenShelterDoesNotExist()
        {
            // Arrange
            var shelterDto = new AnimalShelterDTO { Id = 1, Name = "Updated Shelter" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((AnimalShelter)null);

            // Act
            var result = await _controller.Update(1, shelterDto);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetByLocation_ShouldReturnSheltersInRange()
        {
            // Arrange
            var location = new Point(10, 20){ SRID = 4326 };
            var radius = 5.0;
            var mockShelters = new List<AnimalShelter>
            {
                new AnimalShelter { Id = 1, Name = "Nearby Shelter" }
            };

            _mockRepository.Setup(repo => repo.GetByLocationAsync(location, radius)).ReturnsAsync(mockShelters);

            // Act
            var result = await _controller.GetByLocation(10, 20, 5);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedShelters = Assert.IsType<List<AnimalShelterDTO>>(okResult.Value);
            Assert.Single(returnedShelters);
        }
    }
}
