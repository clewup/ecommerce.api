using ecommerce.api.Controllers;
using ecommerce.api.Infrastructure;
using ecommerce.api.Managers.Contracts;
using ecommerce.api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Moq;

namespace ecommerce.tests.Controllers;

public class ProductControllerTests
{
    [Fact]
    public async void ProductController_GetProducts_Successful()
    {
        var products = new List<ProductModel>();
        
        var mockedLogger = new Mock<ILogger<ProductController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        var mockedProductManager = new Mock<IProductManager>();
        mockedProductManager.Setup(x => x.GetProducts()).ReturnsAsync(products);

        var productController = new ProductController(mockedLogger.Object, mockedClaimsManager.Object, mockedProductManager.Object);

        var result = await productController.GetProducts();
        
        Assert.IsType<OkObjectResult>(result);
    }
    
    [Fact]
    public async void ProductController_GetProductsBySearchCriteria_Successful()
    {
        var products = new List<ProductModel>();
        
        var searchCriteria = new SearchCriteriaModel()
        {
            Category = "PRODUCT_1_CATEGORY"
        };
        
        var mockedLogger = new Mock<ILogger<ProductController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        var mockedProductManager = new Mock<IProductManager>();
        mockedProductManager.Setup(x => x.GetProductsBySearchCriteria(searchCriteria)).ReturnsAsync(products);

        var productController = new ProductController(mockedLogger.Object, mockedClaimsManager.Object, mockedProductManager.Object);

        var result = await productController.GetProductsBySearchCriteria(searchCriteria);
        
        Assert.IsType<OkObjectResult>(result);
    }
    
    [Fact]
    public async void ProductController_GetProductsBySearchCriteria_Unsuccessful()
    {
        var searchCriteria = new SearchCriteriaModel()
        {
            Category = "PRODUCT_1_CATEGORY_INVALID"
        };
        
        var mockedLogger = new Mock<ILogger<ProductController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        var mockedProductManager = new Mock<IProductManager>();

        var productController = new ProductController(mockedLogger.Object, mockedClaimsManager.Object, mockedProductManager.Object);
        productController.ModelState.AddModelError("INVALID_MODEL", "INVALID_MODEL");

        var result = await productController.GetProductsBySearchCriteria(searchCriteria);
        
        Assert.IsType<BadRequestObjectResult>(result);
    }
    
    [Fact]
    public async void ProductController_GetProduct_Successful()
    {
        var product = new ProductModel();
        
        var mockedLogger = new Mock<ILogger<ProductController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        var mockedProductManager = new Mock<IProductManager>();
        mockedProductManager.Setup(x => x.GetProduct(Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA4"))).ReturnsAsync(product);

        var productController = new ProductController(mockedLogger.Object, mockedClaimsManager.Object, mockedProductManager.Object);

        var result = await productController.GetProduct(Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA4"));
        
        Assert.IsType<OkObjectResult>(result);
    }
    
    [Fact]
    public async void ProductController_GetProduct_Unsuccessful()
    {
        var mockedLogger = new Mock<ILogger<ProductController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        var mockedProductManager = new Mock<IProductManager>();

        var productController = new ProductController(mockedLogger.Object, mockedClaimsManager.Object, mockedProductManager.Object);

        var result = await productController.GetProduct(Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA4"));
        
        Assert.IsType<NoContentResult>(result);
    }
    
    [Fact]
    public async void ProductController_CreateProduct_Successful()
    {
        var product = new ProductModel();
        var createdProduct = new ProductModel();
        var user = new UserModel();
        
        var request = new Mock<HttpRequest>();
        var httpContext = Mock.Of<HttpContext>(x => x.Request == request.Object);
        var controllerContext = new ControllerContext() { HttpContext = httpContext, };
        
        var mockedLogger = new Mock<ILogger<ProductController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        mockedClaimsManager.Setup(x => x.GetUser(request.Object)).Returns(user);
        var mockedProductManager = new Mock<IProductManager>();
        mockedProductManager.Setup(x => x.CreateProduct(product, user)).ReturnsAsync(createdProduct);

        var productController = new ProductController(mockedLogger.Object, mockedClaimsManager.Object, mockedProductManager.Object)
        {
            ControllerContext = controllerContext,
        };

        var result = await productController.CreateProduct(product);
        
        Assert.IsType<CreatedResult>(result);
    }
    
    [Fact]
    public async void ProductController_CreateProduct_Unsuccessful()
    {
        var product = new ProductModel();
        
        var mockedLogger = new Mock<ILogger<ProductController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        var mockedProductManager = new Mock<IProductManager>();

        var productController =
            new ProductController(mockedLogger.Object, mockedClaimsManager.Object, mockedProductManager.Object);
        productController.ModelState.AddModelError("INVALID_MODEL", "INVALID_MODEL");

        var result = await productController.CreateProduct(product);
        
        Assert.IsType<BadRequestObjectResult>(result);
    }
    
    [Fact]
    public async void ProductController_UpdateProduct_Successful()
    {
        var product = new ProductModel()
        {
            Id = Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA4"),
        };
        var updatedProduct = new ProductModel();
        var user = new UserModel();
        
        var request = new Mock<HttpRequest>();
        var httpContext = Mock.Of<HttpContext>(x => x.Request == request.Object);
        var controllerContext = new ControllerContext() { HttpContext = httpContext, };
        
        var mockedLogger = new Mock<ILogger<ProductController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        mockedClaimsManager.Setup(x => x.GetUser(request.Object)).Returns(user);
        var mockedProductManager = new Mock<IProductManager>();
        mockedProductManager.Setup(x => x.GetProduct(Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA4")))
            .ReturnsAsync(product);
        mockedProductManager.Setup(x => x.UpdateProduct(product, user)).ReturnsAsync(updatedProduct);

        var productController = new ProductController(mockedLogger.Object, mockedClaimsManager.Object, mockedProductManager.Object)
        {
            ControllerContext = controllerContext,
        };

        var result = await productController.UpdateProduct(product);
        
        Assert.IsType<OkObjectResult>(result);
    }
    
    [Fact]
    public async void ProductController_UpdateProduct_NonExisting_Unsuccessful()
    {
        var product = new ProductModel();
        
        var mockedLogger = new Mock<ILogger<ProductController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        var mockedProductManager = new Mock<IProductManager>();

        var productController =
            new ProductController(mockedLogger.Object, mockedClaimsManager.Object, mockedProductManager.Object);

        var result = await productController.UpdateProduct(product);
        
        Assert.IsType<NoContentResult>(result);
    }
    
    [Fact]
    public async void ProductController_UpdateProduct_ModelState_Unsuccessful()
    {
        var product = new ProductModel();
        
        var mockedLogger = new Mock<ILogger<ProductController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        var mockedProductManager = new Mock<IProductManager>();

        var productController =
            new ProductController(mockedLogger.Object, mockedClaimsManager.Object, mockedProductManager.Object);
        productController.ModelState.AddModelError("INVALID_MODEL", "INVALID_MODEL");

        var result = await productController.UpdateProduct(product);
        
        Assert.IsType<BadRequestObjectResult>(result);
    }
}