using ecommerce.api.Controllers;
using ecommerce.api.Infrastructure;
using ecommerce.api.Managers.Contracts;
using ecommerce.api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace ecommerce.tests.Controllers;

public class OrderControllerTests
{
    [Fact]
    public async void OrderController_GetOrders_Successful()
    {
        var orders = new List<OrderModel>()
        {
            new OrderModel()
            {
                Id = Guid.Parse("AAAE3622-CB7A-4B7B-BFA4-CCCE40D7D162"),
                UserId = Guid.Parse("CDCF7AAC-AA4D-4CD1-9A2C-150E30D83510"),
                FirstName = "ORDER_FIRST_NAME",
                LastName = "ORDER_LAST_NAME",
                Email = "ORDER_EMAIL",
                DeliveryAddress = new AddressModel()
                {
                    LineOne = "ORDER_LINE_ONE",
                    LineTwo = "ORDER_LINE_TWO",
                    LineThree = "ORDER_LINE_THREE",
                    Postcode = "ORDER_POSTCODE",
                    City = "ORDER_CITY",
                    County = "ORDER_COUNTY",
                    Country = "ORDER_COUNTRY",
                },
                Products = new List<ProductModel>(),
            },
            new OrderModel()
            {
                Id = Guid.Parse("DABADE82-0E1A-41C0-9C44-CA592C4D73A4"),
                UserId = Guid.Parse("E51596FF-99FD-41B4-8EF3-BC729B8E5551"),
                FirstName = "ORDER_FIRST_NAME",
                LastName = "ORDER_LAST_NAME",
                Email = "ORDER_EMAIL",
                DeliveryAddress = new AddressModel()
                {
                    LineOne = "ORDER_LINE_ONE",
                    LineTwo = "ORDER_LINE_TWO",
                    LineThree = "ORDER_LINE_THREE",
                    Postcode = "ORDER_POSTCODE",
                    City = "ORDER_CITY",
                    County = "ORDER_COUNTY",
                    Country = "ORDER_COUNTRY",
                },
                Products = new List<ProductModel>(),
            },
        };
            
        var mockedLogger = new Mock<ILogger<OrderController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        var mockedOrderManager = new Mock<IOrderManager>();
        mockedOrderManager.Setup(x => x.GetOrders()).ReturnsAsync(orders);

        var orderController =
            new OrderController(mockedLogger.Object, mockedClaimsManager.Object, mockedOrderManager.Object);

        var result = await orderController.GetOrders();
        
        Assert.IsType<OkObjectResult>(result);
    }
    
    [Fact]
    public async void OrderController_GetUserOrders_Successful()
    {
        var orders = new List<OrderModel>()
        {
            new OrderModel()
            {
                Id = Guid.Parse("AAAE3622-CB7A-4B7B-BFA4-CCCE40D7D162"),
                UserId = Guid.Parse("CDCF7AAC-AA4D-4CD1-9A2C-150E30D83510"),
                FirstName = "ORDER_FIRST_NAME",
                LastName = "ORDER_LAST_NAME",
                Email = "ORDER_EMAIL",
                DeliveryAddress = new AddressModel()
                {
                    LineOne = "ORDER_LINE_ONE",
                    LineTwo = "ORDER_LINE_TWO",
                    LineThree = "ORDER_LINE_THREE",
                    Postcode = "ORDER_POSTCODE",
                    City = "ORDER_CITY",
                    County = "ORDER_COUNTY",
                    Country = "ORDER_COUNTRY",
                },
                Products = new List<ProductModel>(),
            },
            new OrderModel()
            {
                Id = Guid.Parse("DABADE82-0E1A-41C0-9C44-CA592C4D73A4"),
                UserId = Guid.Parse("E51596FF-99FD-41B4-8EF3-BC729B8E5551"),
                FirstName = "ORDER_FIRST_NAME",
                LastName = "ORDER_LAST_NAME",
                Email = "ORDER_EMAIL",
                DeliveryAddress = new AddressModel()
                {
                    LineOne = "ORDER_LINE_ONE",
                    LineTwo = "ORDER_LINE_TWO",
                    LineThree = "ORDER_LINE_THREE",
                    Postcode = "ORDER_POSTCODE",
                    City = "ORDER_CITY",
                    County = "ORDER_COUNTY",
                    Country = "ORDER_COUNTRY",
                },
                Products = new List<ProductModel>(),
            },
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
        
        var mockedLogger = new Mock<ILogger<OrderController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        mockedClaimsManager.Setup(x => x.GetUser(request.Object)).Returns(user);
        var mockedOrderManager = new Mock<IOrderManager>();
        mockedOrderManager.Setup(x => x.GetUserOrders(user)).ReturnsAsync(orders);

        var orderController =
            new OrderController(mockedLogger.Object, mockedClaimsManager.Object, mockedOrderManager.Object)
            {
                ControllerContext = controllerContext,
            };

        var result = await orderController.GetOrders();
        
        Assert.IsType<OkObjectResult>(result);
    }
    
    [Fact]
    public async void OrderController_GetOrder_Successful()
    {
        var order = new OrderModel()
        {
            Id = Guid.Parse("AAAE3622-CB7A-4B7B-BFA4-CCCE40D7D162"),
            UserId = Guid.Parse("CDCF7AAC-AA4D-4CD1-9A2C-150E30D83510"),
            FirstName = "ORDER_FIRST_NAME",
            LastName = "ORDER_LAST_NAME",
            Email = "ORDER_EMAIL",
            DeliveryAddress = new AddressModel()
            {
                LineOne = "ORDER_LINE_ONE",
                LineTwo = "ORDER_LINE_TWO",
                LineThree = "ORDER_LINE_THREE",
                Postcode = "ORDER_POSTCODE",
                City = "ORDER_CITY",
                County = "ORDER_COUNTY",
                Country = "ORDER_COUNTRY",
            },
            Products = new List<ProductModel>(),
        };
            
        var mockedLogger = new Mock<ILogger<OrderController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        var mockedOrderManager = new Mock<IOrderManager>();
        mockedOrderManager.Setup(x => x.GetOrder(Guid.Parse("AAAE3622-CB7A-4B7B-BFA4-CCCE40D7D162"))).ReturnsAsync(order);

        var orderController =
            new OrderController(mockedLogger.Object, mockedClaimsManager.Object, mockedOrderManager.Object);

        var result = await orderController.GetOrder(Guid.Parse("AAAE3622-CB7A-4B7B-BFA4-CCCE40D7D162"));
        
        Assert.IsType<OkObjectResult>(result);
    }
    
    [Fact]
    public async void OrderController_GetOrder_Unsuccessful()
    {
        var mockedLogger = new Mock<ILogger<OrderController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        var mockedOrderManager = new Mock<IOrderManager>();

        var orderController =
            new OrderController(mockedLogger.Object, mockedClaimsManager.Object, mockedOrderManager.Object);

        var result = await orderController.GetOrder(Guid.Parse("AAAE3622-CB7A-4B7B-BFA4-CCCE40D7D162"));
        
        Assert.IsType<NoContentResult>(result);
    }
    
    [Fact]
    public async void OrderController_CreateOrder_Successful()
    {
        var order = new OrderModel()
        {
            Id = Guid.Parse("AAAE3622-CB7A-4B7B-BFA4-CCCE40D7D162"),
            UserId = Guid.Parse("CDCF7AAC-AA4D-4CD1-9A2C-150E30D83510"),
            FirstName = "ORDER_FIRST_NAME",
            LastName = "ORDER_LAST_NAME",
            Email = "ORDER_EMAIL",
            DeliveryAddress = new AddressModel()
            {
                LineOne = "ORDER_LINE_ONE",
                LineTwo = "ORDER_LINE_TWO",
                LineThree = "ORDER_LINE_THREE",
                Postcode = "ORDER_POSTCODE",
                City = "ORDER_CITY",
                County = "ORDER_COUNTY",
                Country = "ORDER_COUNTRY",
            },
            Products = new List<ProductModel>(),
        };
        var createdOrder = new OrderModel()
        {
            Id = Guid.Parse("AAAE3622-CB7A-4B7B-BFA4-CCCE40D7D162"),
            UserId = Guid.Parse("CDCF7AAC-AA4D-4CD1-9A2C-150E30D83510"),
            FirstName = "ORDER_FIRST_NAME",
            LastName = "ORDER_LAST_NAME",
            Email = "ORDER_EMAIL",
            DeliveryAddress = new AddressModel()
            {
                LineOne = "ORDER_LINE_ONE",
                LineTwo = "ORDER_LINE_TWO",
                LineThree = "ORDER_LINE_THREE",
                Postcode = "ORDER_POSTCODE",
                City = "ORDER_CITY",
                County = "ORDER_COUNTY",
                Country = "ORDER_COUNTRY",
            },
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
        
        var mockedLogger = new Mock<ILogger<OrderController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        mockedClaimsManager.Setup(x => x.GetUser(request.Object)).Returns(user);
        var mockedOrderManager = new Mock<IOrderManager>();
        mockedOrderManager.Setup(x => x.CreateOrder(order, user)).ReturnsAsync(createdOrder);

        var orderController =
            new OrderController(mockedLogger.Object, mockedClaimsManager.Object, mockedOrderManager.Object)
            {
                ControllerContext = controllerContext,
            };

        var result = await orderController.CreateOrder(order);
        
        Assert.IsType<CreatedResult>(result);
    }
    
    [Fact]
    public async void OrderController_CreateOrder_Unsuccessful()
    {
        var order = new OrderModel()
        {
            Id = Guid.Parse("AAAE3622-CB7A-4B7B-BFA4-CCCE40D7D162"),
            UserId = Guid.Parse("CDCF7AAC-AA4D-4CD1-9A2C-150E30D83510"),
            FirstName = "ORDER_FIRST_NAME",
            LastName = "ORDER_LAST_NAME",
            Email = "ORDER_EMAIL",
            DeliveryAddress = new AddressModel()
            {
                LineOne = "ORDER_LINE_ONE",
                LineTwo = "ORDER_LINE_TWO",
                LineThree = "ORDER_LINE_THREE",
                Postcode = "ORDER_POSTCODE",
                City = "ORDER_CITY",
                County = "ORDER_COUNTY",
                Country = "ORDER_COUNTRY",
            },
            Products = new List<ProductModel>(),
        };
            
        var mockedLogger = new Mock<ILogger<OrderController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        var mockedOrderManager = new Mock<IOrderManager>();

        var orderController =
            new OrderController(mockedLogger.Object, mockedClaimsManager.Object, mockedOrderManager.Object);
        orderController.ModelState.AddModelError("INVALID_MODEL", "INVALID_MODEL");

        var result = await orderController.UpdateOrder(order);
        
        Assert.IsType<BadRequestObjectResult>(result);
    }
    
    [Fact]
    public async void OrderController_UpdateOrder_Successful()
    {
        var order = new OrderModel()
        {
            Id = Guid.Parse("AAAE3622-CB7A-4B7B-BFA4-CCCE40D7D162"),
            UserId = Guid.Parse("CDCF7AAC-AA4D-4CD1-9A2C-150E30D83510"),
            FirstName = "ORDER_FIRST_NAME",
            LastName = "ORDER_LAST_NAME",
            Email = "ORDER_EMAIL",
            DeliveryAddress = new AddressModel()
            {
                LineOne = "ORDER_LINE_ONE",
                LineTwo = "ORDER_LINE_TWO",
                LineThree = "ORDER_LINE_THREE",
                Postcode = "ORDER_POSTCODE",
                City = "ORDER_CITY",
                County = "ORDER_COUNTY",
                Country = "ORDER_COUNTRY",
            },
            Products = new List<ProductModel>(),
        };
        var updatedOrder = new OrderModel()
        {
            Id = Guid.Parse("AAAE3622-CB7A-4B7B-BFA4-CCCE40D7D162"),
            UserId = Guid.Parse("CDCF7AAC-AA4D-4CD1-9A2C-150E30D83510"),
            FirstName = "ORDER_FIRST_NAME",
            LastName = "ORDER_LAST_NAME",
            Email = "ORDER_EMAIL",
            DeliveryAddress = new AddressModel()
            {
                LineOne = "ORDER_LINE_ONE",
                LineTwo = "ORDER_LINE_TWO",
                LineThree = "ORDER_LINE_THREE",
                Postcode = "ORDER_POSTCODE",
                City = "ORDER_CITY",
                County = "ORDER_COUNTY",
                Country = "ORDER_COUNTRY",
            },
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
        
        var mockedLogger = new Mock<ILogger<OrderController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        mockedClaimsManager.Setup(x => x.GetUser(request.Object)).Returns(user);
        var mockedOrderManager = new Mock<IOrderManager>();
        mockedOrderManager.Setup(x => x.GetOrder(Guid.Parse("AAAE3622-CB7A-4B7B-BFA4-CCCE40D7D162"))).ReturnsAsync(order);
        mockedOrderManager.Setup(x => x.UpdateOrder(order, user)).ReturnsAsync(updatedOrder);

        var orderController =
            new OrderController(mockedLogger.Object, mockedClaimsManager.Object, mockedOrderManager.Object)
            {
                ControllerContext = controllerContext,
            };

        var result = await orderController.UpdateOrder(order);
        
        Assert.IsType<OkObjectResult>(result);
    }
    
    [Fact]
    public async void OrderController_UpdateOrder_NonExisting_Unsuccessful()
    {
        var order = new OrderModel()
        {
            Id = Guid.Parse("AAAE3622-CB7A-4B7B-BFA4-CCCE40D7D162"),
            UserId = Guid.Parse("CDCF7AAC-AA4D-4CD1-9A2C-150E30D83510"),
            FirstName = "ORDER_FIRST_NAME",
            LastName = "ORDER_LAST_NAME",
            Email = "ORDER_EMAIL",
            DeliveryAddress = new AddressModel()
            {
                LineOne = "ORDER_LINE_ONE",
                LineTwo = "ORDER_LINE_TWO",
                LineThree = "ORDER_LINE_THREE",
                Postcode = "ORDER_POSTCODE",
                City = "ORDER_CITY",
                County = "ORDER_COUNTY",
                Country = "ORDER_COUNTRY",
            },
            Products = new List<ProductModel>(),
        };
        
        var mockedLogger = new Mock<ILogger<OrderController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        var mockedOrderManager = new Mock<IOrderManager>();

        var orderController =
            new OrderController(mockedLogger.Object, mockedClaimsManager.Object, mockedOrderManager.Object);

        var result = await orderController.UpdateOrder(order);
        
        Assert.IsType<NoContentResult>(result);
    }
    
    [Fact]
    public async void OrderController_UpdateOrder_ModelState_Unsuccessful()
    {
        var order = new OrderModel()
        {
            Id = Guid.Parse("AAAE3622-CB7A-4B7B-BFA4-CCCE40D7D162"),
            UserId = Guid.Parse("CDCF7AAC-AA4D-4CD1-9A2C-150E30D83510"),
            FirstName = "ORDER_FIRST_NAME",
            LastName = "ORDER_LAST_NAME",
            Email = "ORDER_EMAIL",
            DeliveryAddress = new AddressModel()
            {
                LineOne = "ORDER_LINE_ONE",
                LineTwo = "ORDER_LINE_TWO",
                LineThree = "ORDER_LINE_THREE",
                Postcode = "ORDER_POSTCODE",
                City = "ORDER_CITY",
                County = "ORDER_COUNTY",
                Country = "ORDER_COUNTRY",
            },
            Products = new List<ProductModel>(),
        };
            
        var mockedLogger = new Mock<ILogger<OrderController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        var mockedOrderManager = new Mock<IOrderManager>();

        var orderController =
            new OrderController(mockedLogger.Object, mockedClaimsManager.Object, mockedOrderManager.Object);
        orderController.ModelState.AddModelError("INVALID_MODEL", "INVALID_MODEL");

        var result = await orderController.UpdateOrder(order);
        
        Assert.IsType<BadRequestObjectResult>(result);
    }
}