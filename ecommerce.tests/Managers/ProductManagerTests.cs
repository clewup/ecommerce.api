using AutoMapper;
using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Entities;
using ecommerce.api.Infrastructure;
using ecommerce.api.Managers;
using ecommerce.api.Models;
using Moq;

namespace ecommerce.tests.Managers;

public class ProductManagerTests
{
    [Fact]
    public async void ProductManager_GetProducts_Successful()
    {
        var products = new List<ProductEntity>();
        
        var mockedProductDataManager = new Mock<IProductDataManager>();
        mockedProductDataManager.Setup(x => x.GetProducts()).ReturnsAsync(products);
        var mockedImageDataManager = new Mock<IImageDataManager>();

        var productManager = new ProductManager(mockedProductDataManager.Object,
            mockedImageDataManager.Object);

        var result = await productManager.GetProducts();
        
        Assert.Equal(2, result.Count);
    }
    
    [Fact]
    public async void ProductManager_GetProductsBySearchCriteria_Successful()
    {
        var searchCriteria = new SearchCriteriaModel()
        {
            SearchTerm = "PRODUCT",
        };
        var products = new List<ProductEntity>();
        
        var mockedProductDataManager = new Mock<IProductDataManager>();
        mockedProductDataManager.Setup(x => x.GetProductsBySearchCriteria(searchCriteria)).ReturnsAsync(products);
        var mockedImageDataManager = new Mock<IImageDataManager>();

        var productManager = new ProductManager(mockedProductDataManager.Object,
            mockedImageDataManager.Object);

        var result = await productManager.GetProductsBySearchCriteria(searchCriteria);
        
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async void ProductManager_GetProduct_Successful()
    {
        var product = new ProductEntity();
        var mappedProduct = new ProductModel();
        
        var mockedProductDataManager = new Mock<IProductDataManager>();
        mockedProductDataManager.Setup(x => x.GetProduct(Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA4"))).ReturnsAsync(product);
        var mockedImageDataManager = new Mock<IImageDataManager>();

        var productManager = new ProductManager(mockedProductDataManager.Object,
            mockedImageDataManager.Object);

        var result = await productManager.GetProduct(Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA4"));
        
        Assert.Equal("PRODUCT_1_NAME", result?.Name);
        Assert.Equal("PRODUCT_1_DESCRIPTION", result?.Description);
        Assert.Equal("PRODUCT_1_CATEGORY", result?.Category);
        Assert.Equal("PRODUCT_1_RANGE", result?.Range);
        Assert.Equal("PRODUCT_1_COLOR", result?.Color);
        Assert.Equal(30, result?.Price);
        Assert.Equal(0, result?.Discount);
    }
    
    [Fact]
    public async void ProductManager_CreateProduct_Successful()
    {
        var createdProduct = new ProductModel();
        var product = new ProductEntity();
        var mappedProduct = new ProductModel();
        var user = new UserModel
        {
            Id = Guid.Parse("1F9064A7-8FE6-4BAB-9EE6-37056FF731D3"),
            FirstName = "USER_FIRST_NAME",
            LastName = "USER_LAST_NAME",
            Email = "USER_EMAIL",
            Role = RoleType.User,
            LineOne = "USER_LINE_ONE",
            LineTwo = "USER_LINE_TWO",
            LineThree = "USER_LINE_THREE",
            Postcode = "USER_POSTCODE",
            City = "USER_CITY",
            County = "USER_COUNTY",
            Country = "USER_COUNTRY"
        };
        
        var mockedProductDataManager = new Mock<IProductDataManager>();
        mockedProductDataManager.Setup(x => x.CreateProduct(createdProduct, user)).ReturnsAsync(product);
        var mockedImageDataManager = new Mock<IImageDataManager>();

        var productManager = new ProductManager(mockedProductDataManager.Object,
            mockedImageDataManager.Object);

        var result = await productManager.CreateProduct(createdProduct, user);
        
        Assert.Equal("PRODUCT_NAME", result?.Name);
        Assert.Equal("PRODUCT_DESCRIPTION", result?.Description);
        Assert.Equal("PRODUCT_CATEGORY", result?.Category);
        Assert.Equal("PRODUCT_RANGE", result?.Range);
        Assert.Equal("PRODUCT_COLOR", result?.Color);
        Assert.Equal(30, result?.Price);
        Assert.Equal(0, result?.Discount);
    }
    
    [Fact]
    public async void ProductManager_UpdateProduct_Successful()
    {
        var updatedProduct = new ProductModel();
        var product = new ProductEntity();
        var mappedProduct = new ProductModel();
        var user = new UserModel
        {
            Id = Guid.Parse("1F9064A7-8FE6-4BAB-9EE6-37056FF731D3"),
            FirstName = "USER_FIRST_NAME",
            LastName = "USER_LAST_NAME",
            Email = "USER_EMAIL",
            Role = RoleType.User,
            LineOne = "USER_LINE_ONE",
            LineTwo = "USER_LINE_TWO",
            LineThree = "USER_LINE_THREE",
            Postcode = "USER_POSTCODE",
            City = "USER_CITY",
            County = "USER_COUNTY",
            Country = "USER_COUNTRY"
        };
        
        var mockedProductDataManager = new Mock<IProductDataManager>();
        mockedProductDataManager.Setup(x => x.CreateProduct(updatedProduct, user)).ReturnsAsync(product);
        var mockedImageDataManager = new Mock<IImageDataManager>();

        var productManager = new ProductManager(mockedProductDataManager.Object,
            mockedImageDataManager.Object);

        var result = await productManager.CreateProduct(updatedProduct, user);
        
        Assert.Equal("PRODUCT_NAME", result?.Name);
        Assert.Equal("PRODUCT_DESCRIPTION", result?.Description);
        Assert.Equal("PRODUCT_CATEGORY", result?.Category);
        Assert.Equal("PRODUCT_RANGE", result?.Range);
        Assert.Equal("PRODUCT_COLOR", result?.Color);
        Assert.Equal(30, result?.Price);
        Assert.Equal(0, result?.Discount);
    }
}