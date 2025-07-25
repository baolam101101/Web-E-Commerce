using System;
using AutoMapper;
using Azure.Core;
using Moq;
using Web_E_Commerce.DTOs.Client.Product.Requests;
using Web_E_Commerce.Exceptions;
using Web_E_Commerce.Mapping;
using Web_E_Commerce.Models;
using Web_E_Commerce.Repositories.Interfaces;
using Web_E_Commerce.Services.Implementations;
using Xunit;

namespace Web_E_Commerce.Tests.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly IMapper _mapper;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockCategoryRepository = new Mock<ICategoryRepository>();

            // Config AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductProfile>();
            });
            _mapper = config.CreateMapper();

            // Init service with mock and mapper
            _productService = new ProductService(
                _mockProductRepository.Object,
                _mockCategoryRepository.Object,
                _mapper);
        }

        // ===========================
        // CREATE Product Tests
        // ===========================
        [Fact]
        public async Task CreateAsync_ShouldReturnProduct_WhenSuccessful()
        {
            // Arrange
            var request = new ProductCreateRequest
            {
                Name = "Test",
                Description = "Desc",
                Price = 100,
                CategoryId = 1
            };

            var category = new Category
            {
                Id = request.CategoryId,
                Name = "CategoryName"
            };

            var createdProduct = new Product
            {
                Id = 1,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CategoryId = request.CategoryId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "system"
            };

            _mockCategoryRepository
                .Setup(repo => repo.GetByIdAsync(request.CategoryId))
                .ReturnsAsync(category);

            _mockProductRepository
                .Setup(repo => repo.CreateAsync(It.IsAny<Product>()))
                .ReturnsAsync(createdProduct);

            // Act
            var result = await _productService.CreateAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Product created successfully", result.Message);
            Assert.Equal("Test", result.Data?.Name);
            Assert.Equal(100, result.Data?.Price);
        }

        // ===========================
        // READ Product Tests
        // ===========================
        [Fact]
        public async Task GetByIdAsync_ShouldReturnProduct_WhenExists()
        {
            // Arrange
            var productId = 1;
            var product = new Product { Id = productId, Name = "Test" };

            _mockProductRepository
                .Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync(product);

            // Act
            var result = await _productService.GetByIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productId, result.Data?.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotFound()
        {
            // Arrange
            var productId = 999;

            _mockProductRepository
                .Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync((Product?)null);

            // Act

            var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
                _productService.GetByIdAsync(productId));

            // Assert
            Assert.Equal("Product not found", exception.Message);
        }

        // ===========================
        // UPDATE Product Tests
        // ===========================
        [Fact]
        public async Task UpdateAsync_ShouldReturnUpdatedProduct_WhenSuccessful()
        {
            // Arrange
            var productId = 1;
            var existingProduct = new Product { Id = productId, Name = "Old" };

            var request = new ProductUpdateRequest
            {
                Name = "New Name",
                Description = "Updated desc",
                Price = 200,
                CategoryId = 2
            };

            var category = new Category
            {
                Id = request.CategoryId,
                Name = "CategoryName"
            };

            _mockCategoryRepository
                .Setup(repo => repo.GetByIdAsync(request.CategoryId))
                .ReturnsAsync(category);

            _mockProductRepository
                .Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync(existingProduct);

            _mockProductRepository
                .Setup(repo => repo.UpdateAsync(It.IsAny<Product>()))
                .Returns((Product p) => Task.FromResult(p))
                .Verifiable();

            // Act
            var result = await _productService.UpdateAsync(productId, request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("New Name", result.Data?.Name);
            Assert.Equal(200, result.Data?.Price);

            _mockProductRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnNull_WhenProductNotFound()
        {
            // Arrange
            var productId = 999;
            var request = new ProductUpdateRequest
            {
                Name = "Test",
                Description = "Desc",
                Price = 100,
                CategoryId = 1
            };

            _mockProductRepository
                .Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync((Product?)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
                _productService.UpdateAsync(productId, request));

            Assert.Equal("Product not found", exception.Message);

        }

        // ===========================
        // DELETE Product Tests
        // ===========================
        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue_WhenSuccessful()
        {
            // Arrange
            var productId = 1;
            var product = new Product { Id = productId };

            _mockProductRepository
                .Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync(product);

            _mockProductRepository
                .Setup(repo => repo.DeleteAsync(It.IsAny<Product>()))
                .Returns(Task.FromResult(true));

            // Act
            var result = await _productService.DeleteAsync(productId);

            // Assert
            Assert.True(result.Data);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenProductNotFound()
        {
            // Arrange
            var productId = 999;

            _mockProductRepository
                .Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync((Product?)null);

            // Act
            var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
                _productService.DeleteAsync(productId));

            Assert.Equal("Product not found", exception.Message);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenDeleteFails()
        {
            // Arrange
            var productId = 1;
            var product = new Product { Id = productId };

            _mockProductRepository
                .Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync(product);

            _mockProductRepository
                .Setup(repo => repo.DeleteAsync(It.IsAny<Product>()))
                .Returns(Task.FromResult(false)); // giả lập lỗi xóa

            // Act
            var result = await _productService.DeleteAsync(productId);

            // Assert
            Assert.False(result.Data);
        }
    }
}