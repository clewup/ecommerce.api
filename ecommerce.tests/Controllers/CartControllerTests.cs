using ecommerce.api.Controllers;
using ecommerce.api.Infrastructure;
using ecommerce.api.Managers.Contracts;
using ecommerce.api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace ecommerce.tests.Controllers;

public class CartControllerTests
{
    [Fact]
    public async void CartController_GetCart_Successful()
    {
        var cart = new CartModel()
        {
            Id = Guid.Parse("D7447BFA-6CBC-406F-8EE1-B85226C8081F"),
            UserId = Guid.Parse("1F9064A7-8FE6-4BAB-9EE6-37056FF731D3"),
            Total = 59.99,
            Products = new List<ProductModel>(),
        };
        
        var mockedLogger = new Mock<ILogger<CartController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        var mockedCartManager = new Mock<ICartManager>();
        mockedCartManager.Setup(x => x.GetCart(Guid.Parse("D7447BFA-6CBC-406F-8EE1-B85226C8081F"))).ReturnsAsync(cart);

        var cartController =
            new CartController(mockedLogger.Object, mockedClaimsManager.Object, mockedCartManager.Object);

        var result = await cartController.GetCart(Guid.Parse("D7447BFA-6CBC-406F-8EE1-B85226C8081F"));
        
        Assert.IsType<OkObjectResult>(result);
    }
    
    [Fact]
    public async void CartController_GetCart_Unsuccessful()
    {
        var mockedLogger = new Mock<ILogger<CartController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        var mockedCartManager = new Mock<ICartManager>();

        var cartController =
            new CartController(mockedLogger.Object, mockedClaimsManager.Object, mockedCartManager.Object);

        var result = await cartController.GetCart(Guid.Parse("D7447BFA-6CBC-406F-8EE1-B85226C8081F"));
        
        Assert.IsType<NoContentResult>(result);
    }
    
    [Fact]
    public async void CartController_GetUserCart_Successful()
    {
        var cart = new CartModel()
        {
            Id = Guid.Parse("D7447BFA-6CBC-406F-8EE1-B85226C8081F"),
            UserId = Guid.Parse("1F9064A7-8FE6-4BAB-9EE6-37056FF731D3"),
            Total = 59.99,
            Products = new List<ProductModel>(),
        };
        var user = new UserModel()
        {
            Id = Guid.Parse("1F9064A7-8FE6-4BAB-9EE6-37056FF731D3"),
            Email = "USER_EMAIL",
            Role = RoleType.User,
        };

        var request = new Mock<HttpRequest>();
        var httpContext = Mock.Of<HttpContext>(x => x.Request == request.Object);
        var controllerContext = new ControllerContext() { HttpContext = httpContext, };
        
        var mockedLogger = new Mock<ILogger<CartController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        mockedClaimsManager.Setup(x => x.GetUser(request.Object)).Returns(user);
        var mockedCartManager = new Mock<ICartManager>();
        mockedCartManager.Setup(x => x.GetUserCart(user)).ReturnsAsync(cart);

        var cartController =
            new CartController(mockedLogger.Object, mockedClaimsManager.Object, mockedCartManager.Object)
            {
                ControllerContext = controllerContext,
            };

        var result = await cartController.GetUserCart();
        
        Assert.IsType<OkObjectResult>(result);
    }
    
    [Fact]
    public async void CartController_GetUserCart_Unsuccessful()
    {
        var mockedLogger = new Mock<ILogger<CartController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        var mockedCartManager = new Mock<ICartManager>();

        var cartController =
            new CartController(mockedLogger.Object, mockedClaimsManager.Object, mockedCartManager.Object);

        var result = await cartController.GetUserCart();
        
        Assert.IsType<NoContentResult>(result);
    }
    
    [Fact]
    public async void CartController_CreateCart_Successful()
    {
        var cart = new CartModel()
        {
            Id = Guid.Parse("D7447BFA-6CBC-406F-8EE1-B85226C8081F"),
            UserId = Guid.Parse("1F9064A7-8FE6-4BAB-9EE6-37056FF731D3"),
            Total = 59.99,
            Products = new List<ProductModel>(),
        };
        var createdCart = new CartModel()
        {
            Id = Guid.Parse("D7447BFA-6CBC-406F-8EE1-B85226C8081F"),
            UserId = Guid.Parse("1F9064A7-8FE6-4BAB-9EE6-37056FF731D3"),
            Total = 59.99,
            Products = new List<ProductModel>(),
        };
        var user = new UserModel()
        {
            Id = Guid.Parse("1F9064A7-8FE6-4BAB-9EE6-37056FF731D3"),
            Email = "USER_EMAIL",
            Role = RoleType.User,
        };

        var request = new Mock<HttpRequest>();
        var httpContext = Mock.Of<HttpContext>(x => x.Request == request.Object);
        var controllerContext = new ControllerContext() { HttpContext = httpContext, };
        
        var mockedLogger = new Mock<ILogger<CartController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        mockedClaimsManager.Setup(x => x.GetUser(request.Object)).Returns(user);
        var mockedCartManager = new Mock<ICartManager>();
        mockedCartManager.Setup(x => x.CreateCart(cart, user)).ReturnsAsync(createdCart);

        var cartController =
            new CartController(mockedLogger.Object, mockedClaimsManager.Object, mockedCartManager.Object)
            {
                ControllerContext = controllerContext,
            };

        var result = await cartController.CreateCart(cart);
        
        Assert.IsType<CreatedResult>(result);
    }
    
    [Fact]
    public async void CartController_CreateCart_Unsuccessful()
    {
        var cart = new CartModel()
        {
            Id = Guid.Parse("D7447BFA-6CBC-406F-8EE1-B85226C8081F"),
            UserId = Guid.Parse("1F9064A7-8FE6-4BAB-9EE6-37056FF731D3"),
            Total = 59.99,
            Products = new List<ProductModel>(),
        };

        var mockedLogger = new Mock<ILogger<CartController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        var mockedCartManager = new Mock<ICartManager>();

        var cartController =
            new CartController(mockedLogger.Object, mockedClaimsManager.Object, mockedCartManager.Object);
        
        cartController.ModelState.AddModelError("INVALID_MODEL", "INVALID_MODEL");

        var result = await cartController.CreateCart(cart);
        
        Assert.IsType<BadRequestObjectResult>(result);
    }
    
    [Fact]
    public async void CartController_UpdateCart_Successful()
    {
        var cart = new CartModel()
        {
            Id = Guid.Parse("D7447BFA-6CBC-406F-8EE1-B85226C8081F"),
            UserId = Guid.Parse("1F9064A7-8FE6-4BAB-9EE6-37056FF731D3"),
            Total = 59.99,
            Products = new List<ProductModel>(),
        };
        var createdCart = new CartModel()
        {
            Id = Guid.Parse("D7447BFA-6CBC-406F-8EE1-B85226C8081F"),
            UserId = Guid.Parse("1F9064A7-8FE6-4BAB-9EE6-37056FF731D3"),
            Total = 59.99,
            Products = new List<ProductModel>(),
        };
        var user = new UserModel()
        {
            Id = Guid.Parse("1F9064A7-8FE6-4BAB-9EE6-37056FF731D3"),
            Email = "USER_EMAIL",
            Role = RoleType.User,
        };

        var request = new Mock<HttpRequest>();
        var httpContext = Mock.Of<HttpContext>(x => x.Request == request.Object);
        var controllerContext = new ControllerContext() { HttpContext = httpContext, };
        
        var mockedLogger = new Mock<ILogger<CartController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        mockedClaimsManager.Setup(x => x.GetUser(request.Object)).Returns(user);
        var mockedCartManager = new Mock<ICartManager>();
        mockedCartManager.Setup(x => x.GetUserCart(user)).ReturnsAsync(cart);
        mockedCartManager.Setup(x => x.UpdateCart(cart, user)).ReturnsAsync(createdCart);

        var cartController =
            new CartController(mockedLogger.Object, mockedClaimsManager.Object, mockedCartManager.Object)
            {
                ControllerContext = controllerContext,
            };

        var result = await cartController.UpdateCart(cart);
        
        Assert.IsType<OkObjectResult>(result);
    }
    
    [Fact]
    public async void CartController_UpdateCart_NonExisting_Unsuccessful()
    {
        var cart = new CartModel()
        {
            Id = Guid.Parse("D7447BFA-6CBC-406F-8EE1-B85226C8081F"),
            UserId = Guid.Parse("1F9064A7-8FE6-4BAB-9EE6-37056FF731D3"),
            Total = 59.99,
            Products = new List<ProductModel>(),
        };

        var request = new Mock<HttpRequest>();
        var httpContext = Mock.Of<HttpContext>(x => x.Request == request.Object);
        var controllerContext = new ControllerContext() { HttpContext = httpContext, };
        
        var mockedLogger = new Mock<ILogger<CartController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        var mockedCartManager = new Mock<ICartManager>();

        var cartController =
            new CartController(mockedLogger.Object, mockedClaimsManager.Object, mockedCartManager.Object)
            {
                ControllerContext = controllerContext,
            };

        var result = await cartController.UpdateCart(cart);
        
        Assert.IsType<NoContentResult>(result);
    }
    
    [Fact]
    public async void CartController_UpdateCart_ModelState_Unsuccessful()
    {
        var cart = new CartModel()
        {
            Id = Guid.Parse("D7447BFA-6CBC-406F-8EE1-B85226C8081F"),
            UserId = Guid.Parse("1F9064A7-8FE6-4BAB-9EE6-37056FF731D3"),
            Total = 59.99,
            Products = new List<ProductModel>(),
        };

        var mockedLogger = new Mock<ILogger<CartController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        var mockedCartManager = new Mock<ICartManager>();

        var cartController =
            new CartController(mockedLogger.Object, mockedClaimsManager.Object, mockedCartManager.Object);
        
        cartController.ModelState.AddModelError("INVALID_MODEL", "INVALID_MODEL");

        var result = await cartController.UpdateCart(cart);
        
        Assert.IsType<BadRequestObjectResult>(result);
    }
}