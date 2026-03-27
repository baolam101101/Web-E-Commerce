//using AutoMapper;
//using Moq;
//using Web_E_Commerce.DTOs.Client.Product.Requests;
//using Web_E_Commerce.DTOs.Client.Product.Responses;
//using Web_E_Commerce.Exceptions;
//using Web_E_Commerce.Mapping;
//using Web_E_Commerce.Models;
//using Web_E_Commerce.Repositories.Interfaces;
//using Web_E_Commerce.Services.Implementations;
//using Xunit;

//public class ProductServiceTests
//{
//    private readonly Mock<IProductRepositories> _mockRepo;
//    private readonly Mock<ICategoryRepositories> _mockCategoryRepo;
//    private readonly IMapper _mapper;
//    private readonly ProductService _service;

//    public ProductServiceTests()
//    {
//        _mockRepo = new Mock<IProductRepositories>();
//        _mockCategoryRepo = new Mock<ICategoryRepositories>();

//        var config = new MapperConfiguration(cfg =>
//        {
//            cfg.AddProfile<ProductProfile>();
//            // thêm map khác nếu có
//        });
//        _mapper = config.CreateMapper();

//        _service = new ProductService(_mockRepo.Object, _mockCategoryRepo.Object, _mapper);
//    }

//    [Fact]
//    public async Task GetByIdAsync_ReturnsSuccess_WhenProductExists()
//    {
//        // Arrange
//        int id = 1;
//        var product = new Product { Id = id, Name = "TestProduct" };
//        _mockRepo.Setup(r => r.GetByIdAsync(id))
//            .ReturnsAsync(product);

//        // Act
//        var result = await _service.GetByIdAsync(id);

//        // Assert
//        Assert.True(result.Success);
//        Assert.NotNull(result.Data);
//        Assert.Equal(id, result.Data.Id);
//        Assert.Equal("TestProduct", result.Data.Name);
//    }

//    [Fact]
//    public async Task GetByIdAsync_ThrowsNotFound_WhenProductDoesNotExist()
//    {
//        // Arrange
//        int id = 99;
//        _mockRepo.Setup(r => r.GetByIdAsync(id))
//            .ReturnsAsync((Product?)null);

//        // Act & Assert
//        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetByIdAsync(id));
//    }

//    [Fact]
//    public async Task CreateAsync_ReturnsCreatedData()
//    {
//        // Arrange
//        var categoryId = 1;
//        var dto = new ProductCreateRequest { Name = "NewProduct", CategoryId = categoryId };
//        var product = new Product { Id = 2, Name = "NewProduct" };

//        _mockCategoryRepo
//            .Setup(c => c.GetByIdAsync(categoryId))
//            .ReturnsAsync(new Category
//            {
//                Id = categoryId,
//                Name = "Category Test"
//            });

//        _mockRepo.Setup(r => r.CreateAsync(It.IsAny<Product>()))
//            .ReturnsAsync(product);

//        // Act
//        var result = await _service.CreateAsync(dto);

//        // Assert
//        Assert.True(result.Success);
//        Assert.NotNull(result.Data);
//        Assert.Equal("NewProduct", result.Data.Name);
//    }

//    [Fact]
//    public async Task DeleteAsync_ReturnsSuccess_WhenDeleted()
//    {
//        // Arrange
//        int id = 3;
//        var product = new Product { Id = id };
//        _mockRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(product);
//        _mockRepo.Setup(r => r.DeleteAsync(product)).ReturnsAsync(true);

//        // Act
//        var result = await _service.DeleteAsync(id);

//        // Assert
//        Assert.True(result.Success);
//        Assert.True(result.Data);
//    }

//    [Fact]
//    public async Task CreateAsync_CategoryNotFound_ShouldThrowException()
//    {
//        var dto = new ProductCreateRequest
//        {
//            Name = "Test",
//            CategoryId = 999
//        };

//        _mockCategoryRepo
//            .Setup(c => c.GetByIdAsync(999))
//            .ReturnsAsync((Category?)null);

//        await Assert.ThrowsAsync<NotFoundException>(() =>
//            _service.CreateAsync(dto));
//    }

//    [Fact]
//    public async Task UpdateAsync_ReturnsUpdatedData_WhenValid()
//    {
//        // Arrange
//        int productId = 1;
//        int categoryId = 2;

//        var existingProduct = new Product
//        {
//            Id = productId,
//            Name = "Old Name",
//            CategoryId = categoryId
//        };

//        var updateDto = new ProductUpdateRequest
//        {
//            Name = "Updated Name",
//            CategoryId = categoryId
//        };

//        _mockRepo
//            .Setup(r => r.GetByIdAsync(productId))
//            .ReturnsAsync(existingProduct);

//        _mockCategoryRepo
//            .Setup(c => c.GetByIdAsync(categoryId))
//            .ReturnsAsync(new Category
//            {
//                Id = categoryId,
//                Name = "Category Test"
//            });

//        _mockRepo
//            .Setup(r => r.UpdateAsync(existingProduct))
//            .Returns(Task.CompletedTask);

//        // Act
//        var result = await _service.UpdateAsync(productId, updateDto);

//        // Assert
//        Assert.True(result.Success);
//        Assert.NotNull(result.Data);
//        Assert.Equal("Updated Name", result.Data.Name);
//    }

//    [Fact]
//    public async Task UpdateAsync_ProductNotFound_ShouldThrowException()
//    {
//        // Arrange
//        int productId = 99;
//        var dto = new ProductUpdateRequest
//        {
//            Name = "Update",
//            CategoryId = 1
//        };

//        _mockRepo
//            .Setup(r => r.GetByIdAsync(productId))
//            .ReturnsAsync((Product?)null);

//        // Act & Assert
//        await Assert.ThrowsAsync<NotFoundException>(() =>
//            _service.UpdateAsync(productId, dto));
//    }

//    [Fact]
//    public async Task UpdateAsync_CategoryNotFound_ShouldThrowException()
//    {
//        // Arrange
//        int productId = 1;
//        int categoryId = 999;

//        var product = new Product
//        {
//            Id = productId,
//            Name = "Product",
//            CategoryId = categoryId
//        };

//        var dto = new ProductUpdateRequest
//        {
//            Name = "Update",
//            CategoryId = categoryId
//        };

//        _mockRepo
//            .Setup(r => r.GetByIdAsync(productId))
//            .ReturnsAsync(product);

//        _mockCategoryRepo
//            .Setup(c => c.GetByIdAsync(categoryId))
//            .ReturnsAsync((Category?)null);

//        // Act & Assert
//        await Assert.ThrowsAsync<NotFoundException>(() =>
//            _service.UpdateAsync(productId, dto));
//    }


//    // Thêm test cho UpdateAsync, GetAllAsync
//}
