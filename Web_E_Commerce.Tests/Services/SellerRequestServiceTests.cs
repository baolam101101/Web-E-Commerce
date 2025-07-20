using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Web_E_Commerce.Data;
using Web_E_Commerce.DTOs.Admin.SellerRequest.Responses;
using Web_E_Commerce.DTOs.Seller.Requests;
using Web_E_Commerce.Models;
using Web_E_Commerce.Services.Implementations;
using Web_E_Commerce.Services.Interfaces;
using Xunit;

namespace Web_E_Commerce.Tests.Services
{
    public class SellerRequestServiceTests
    {
        private readonly Mock<ICurrentUserService> _mockCurrentUser;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AppDbContext _dbContext;
        private readonly SellerRequestService _service;

        public SellerRequestServiceTests()
        {
            _mockCurrentUser = new Mock<ICurrentUserService>();
            _mockMapper = new Mock<IMapper>();

            // Tạo DbContext in-memory
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _dbContext = new AppDbContext(options);

            _service = new SellerRequestService(
                _mockCurrentUser.Object,
                _mockMapper.Object,
                _dbContext
            );
        }

        [Fact]
        public async Task RequestSellerAsync_ShouldReturnError_WhenRequestAlreadyExists()
        {
            // Arrange
            var userId = 1;
            _mockCurrentUser.Setup(c => c.UserId).Returns(userId);

            _dbContext.SellerRequests.Add(new SellerRequest
            {
                UserId = userId,
                Status = "Pending"
            });
            await _dbContext.SaveChangesAsync();

            var dto = new SellerRequestDto();

            // Act
            var result = await _service.RequestSellerAsync(dto);

            // Assert
            Assert.Equal("You already have a pending seller request", result.Message);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task RequestSellerAsync_ShouldCreateRequest_WhenNoneExists()
        {
            // Arrange
            var userId = 2;
            _mockCurrentUser.Setup(c => c.UserId).Returns(userId);

            var dto = new SellerRequestDto
            {
                ShopName = "Test Shop",
                Note = "Test Desc"
            };

            var requestEntity = new SellerRequest
            {
                ShopName = dto.ShopName,
                Note = dto.Note,
                UserId = userId
            };

            var responseDto = new SellerRequestResponse
            {
                Id = 99,
                ShopName = dto.ShopName,
                Note = dto.Note
            };

            _mockMapper.Setup(m => m.Map<SellerRequest>(dto)).Returns(requestEntity);
            _mockMapper.Setup(m => m.Map<SellerRequestResponse>(It.IsAny<SellerRequest>()))
                       .Returns(responseDto);

            // Act
            var result = await _service.RequestSellerAsync(dto);

            // Assert
            Assert.Equal("Request sent", result.Message);
            Assert.NotNull(result.Data);
            Assert.Equal("Test Shop", result.Data!.ShopName);
        }
    }
}