using AutoMapper;
using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Entities;
using ecommerce.api.Infrastructure;
using ecommerce.api.Managers;
using ecommerce.api.Models;
using Microsoft.AspNetCore.Http;
using Moq;

namespace ecommerce.tests.Managers;

public class OrderManagerTests
{
    [Fact]
    public async void OrderManager_GetOrders_Successful()
    {
        var orders = new List<OrderEntity>()
        {
            new OrderEntity
            {
                Id = Guid.Parse("AAAE3622-CB7A-4B7B-BFA4-CCCE40D7D162"),
                UserId = Guid.Parse("CDCF7AAC-AA4D-4CD1-9A2C-150E30D83510"),
                FirstName = "ORDER_FIRST_NAME",
                LastName = "ORDER_LAST_NAME",
                Email = "ORDER_EMAIL",
                LineOne = "ORDER_LINE_ONE",
                LineTwo = "ORDER_LINE_TWO",
                LineThree = "ORDER_LINE_THREE",
                Postcode = "ORDER_POSTCODE",
                City = "ORDER_CITY",
                County = "ORDER_COUNTY",
                Country = "ORDER_COUNTRY",
                Products = new List<ProductEntity>(),
            },
            new OrderEntity
            {
                Id = Guid.Parse("DABADE82-0E1A-41C0-9C44-CA592C4D73A4"),
                UserId = Guid.Parse("E51596FF-99FD-41B4-8EF3-BC729B8E5551"),
                FirstName = "ORDER_FIRST_NAME",
                LastName = "ORDER_LAST_NAME",
                Email = "ORDER_EMAIL",
                LineOne = "ORDER_LINE_ONE",
                LineTwo = "ORDER_LINE_TWO",
                LineThree = "ORDER_LINE_THREE",
                Postcode = "ORDER_POSTCODE",
                City = "ORDER_CITY",
                County = "ORDER_COUNTY",
                Country = "ORDER_COUNTRY",
                Products = new List<ProductEntity>(),
            },
        };
        
        var mockedOrderDataManager = new Mock<IOrderDataManager>();
        mockedOrderDataManager.Setup(x => x.GetOrders()).ReturnsAsync(orders);
        var mockedProductDataManager = new Mock<IProductDataManager>();

        var orderManager = new OrderManager(mockedOrderDataManager.Object,
            mockedProductDataManager.Object);

        var result = await orderManager.GetOrders();
        
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async void OrderManager_GetUserOrders_Successful()
    {
        var orders = new List<OrderEntity>()
        {
            new OrderEntity
            {
                Id = Guid.Parse("AAAE3622-CB7A-4B7B-BFA4-CCCE40D7D162"),
                UserId = Guid.Parse("CDCF7AAC-AA4D-4CD1-9A2C-150E30D83510"),
                FirstName = "ORDER_FIRST_NAME",
                LastName = "ORDER_LAST_NAME",
                Email = "ORDER_EMAIL",
                LineOne = "ORDER_LINE_ONE",
                LineTwo = "ORDER_LINE_TWO",
                LineThree = "ORDER_LINE_THREE",
                Postcode = "ORDER_POSTCODE",
                City = "ORDER_CITY",
                County = "ORDER_COUNTY",
                Country = "ORDER_COUNTRY",
                Products = new List<ProductEntity>()
            },
        };
        var mappedOrders = new List<OrderModel>()
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
                Products = new List<ProductModel>()
            },
        };
        var user = new UserModel
        {
            Id = Guid.Parse("CDCF7AAC-AA4D-4CD1-9A2C-150E30D83510"),
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
        
        var mockedOrderDataManager = new Mock<IOrderDataManager>();
        mockedOrderDataManager.Setup(x => x.GetUserOrders(user)).ReturnsAsync(orders);
        var mockedProductDataManager = new Mock<IProductDataManager>();

        var orderManager = new OrderManager(mockedOrderDataManager.Object,
            mockedProductDataManager.Object);

        var result = await orderManager.GetUserOrders(user);
        
        Assert.Single(result);
    }
    
    [Fact]
    public async void OrderManager_GetOrder_Successful()
    {
        var order = new OrderEntity
        {
            Id = Guid.Parse("AAAE3622-CB7A-4B7B-BFA4-CCCE40D7D162"),
            UserId = Guid.Parse("CDCF7AAC-AA4D-4CD1-9A2C-150E30D83510"),
            FirstName = "ORDER_FIRST_NAME",
            LastName = "ORDER_LAST_NAME",
            Email = "ORDER_EMAIL",
            LineOne = "ORDER_LINE_ONE",
            LineTwo = "ORDER_LINE_TWO",
            LineThree = "ORDER_LINE_THREE",
            Postcode = "ORDER_POSTCODE",
            City = "ORDER_CITY",
            County = "ORDER_COUNTY",
            Country = "ORDER_COUNTRY",
            Products = new List<ProductEntity>()
        };
       
        var mockedOrderDataManager = new Mock<IOrderDataManager>();
        mockedOrderDataManager.Setup(x => x.GetOrder(Guid.Parse("AAAE3622-CB7A-4B7B-BFA4-CCCE40D7D162")))
            .ReturnsAsync(order);
        var mockedProductDataManager = new Mock<IProductDataManager>();

        var orderManager = new OrderManager(mockedOrderDataManager.Object,
            mockedProductDataManager.Object);

        var result = await orderManager.GetOrder(Guid.Parse("AAAE3622-CB7A-4B7B-BFA4-CCCE40D7D162"));
        
        Assert.Equal("ORDER_FIRST_NAME", result?.FirstName);
        Assert.Equal("ORDER_LAST_NAME", result?.LastName);
        Assert.Equal("ORDER_EMAIL", result?.Email);
        Assert.Equal("ORDER_LINE_ONE", result?.DeliveryAddress.LineOne);
        Assert.Equal("ORDER_LINE_TWO", result?.DeliveryAddress.LineTwo);
        Assert.Equal("ORDER_LINE_THREE", result?.DeliveryAddress.LineThree);
        Assert.Equal("ORDER_POSTCODE", result?.DeliveryAddress.Postcode);
        Assert.Equal("ORDER_CITY", result?.DeliveryAddress.City);
        Assert.Equal("ORDER_COUNTY", result?.DeliveryAddress.County);
        Assert.Equal("ORDER_COUNTRY", result?.DeliveryAddress.Country);
    }
    
    [Fact]
    public async void OrderManager_CreateOrder_Successful()
    {
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
        var order = new OrderEntity
        {
            Id = Guid.Parse("AAAE3622-CB7A-4B7B-BFA4-CCCE40D7D162"),
            UserId = Guid.Parse("CDCF7AAC-AA4D-4CD1-9A2C-150E30D83510"),
            FirstName = "ORDER_FIRST_NAME",
            LastName = "ORDER_LAST_NAME",
            Email = "ORDER_EMAIL",
            LineOne = "ORDER_LINE_ONE",
            LineTwo = "ORDER_LINE_TWO",
            LineThree = "ORDER_LINE_THREE",
            Postcode = "ORDER_POSTCODE",
            City = "ORDER_CITY",
            County = "ORDER_COUNTY",
            Country = "ORDER_COUNTRY",
            Products = new List<ProductEntity>(),
        };
        var mappedOrder = new OrderModel()
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
        var products = new List<ProductEntity>();
        var user = new UserModel
        {
            Id = Guid.Parse("CDCF7AAC-AA4D-4CD1-9A2C-150E30D83510"),
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
        
        var mockedOrderDataManager = new Mock<IOrderDataManager>();
        mockedOrderDataManager.Setup(x => x.GetOrder(Guid.Parse("AAAE3622-CB7A-4B7B-BFA4-CCCE40D7D162")))
            .ReturnsAsync(order);
        mockedOrderDataManager.Setup(x => x.CreateOrder(createdOrder, user)).ReturnsAsync(order);
        var mockedProductDataManager = new Mock<IProductDataManager>();
        mockedProductDataManager.Setup(x => x.GetProducts(order)).ReturnsAsync(products);

        var orderManager = new OrderManager(mockedOrderDataManager.Object,
            mockedProductDataManager.Object);

        var result = await orderManager.CreateOrder(createdOrder, user);
        
        Assert.Equal("ORDER_FIRST_NAME", result?.FirstName);
        Assert.Equal("ORDER_LAST_NAME", result?.LastName);
        Assert.Equal("ORDER_EMAIL", result?.Email);
        Assert.Equal("ORDER_LINE_ONE", result?.DeliveryAddress.LineOne);
        Assert.Equal("ORDER_LINE_TWO", result?.DeliveryAddress.LineTwo);
        Assert.Equal("ORDER_LINE_THREE", result?.DeliveryAddress.LineThree);
        Assert.Equal("ORDER_POSTCODE", result?.DeliveryAddress.Postcode);
        Assert.Equal("ORDER_CITY", result?.DeliveryAddress.City);
        Assert.Equal("ORDER_COUNTY", result?.DeliveryAddress.County);
        Assert.Equal("ORDER_COUNTRY", result?.DeliveryAddress.Country);
    }
    
    [Fact]
    public async void OrderManager_CreateOrder_Unsuccessful()
    {
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
        var order = new OrderEntity
        {
            Id = Guid.Parse("AAAE3622-CB7A-4B7B-BFA4-CCCE40D7D162"),
            UserId = Guid.Parse("CDCF7AAC-AA4D-4CD1-9A2C-150E30D83510"),
            FirstName = "ORDER_FIRST_NAME",
            LastName = "ORDER_LAST_NAME",
            Email = "ORDER_EMAIL",
            LineOne = "ORDER_LINE_ONE",
            LineTwo = "ORDER_LINE_TWO",
            LineThree = "ORDER_LINE_THREE",
            Postcode = "ORDER_POSTCODE",
            City = "ORDER_CITY",
            County = "ORDER_COUNTY",
            Country = "ORDER_COUNTRY",
            Products = new List<ProductEntity>(),
        };
        var products = new List<ProductEntity>();
        var user = new UserModel
        {
            Id = Guid.Parse("CDCF7AAC-AA4D-4CD1-9A2C-150E30D83510"),
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
        
        var mockedOrderDataManager = new Mock<IOrderDataManager>();
        mockedOrderDataManager.Setup(x => x.GetOrder(Guid.Parse("AAAE3622-CB7A-4B7B-BFA4-CCCE40D7D162")))
            .ReturnsAsync(order);
        mockedOrderDataManager.Setup(x => x.CreateOrder(createdOrder, user)).ReturnsAsync(order);
        var mockedProductDataManager = new Mock<IProductDataManager>();
        mockedProductDataManager.Setup(x => x.GetProducts(order)).ReturnsAsync(products);

        var orderManager = new OrderManager(mockedOrderDataManager.Object,
            mockedProductDataManager.Object);

        await Assert.ThrowsAsync<BadHttpRequestException>(() => orderManager.CreateOrder(createdOrder, user));
    }
    
    [Fact]
    public async void OrderManager_UpdateOrder_Successful()
    {
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
        var order = new OrderEntity
        {
            Id = Guid.Parse("AAAE3622-CB7A-4B7B-BFA4-CCCE40D7D162"),
            UserId = Guid.Parse("CDCF7AAC-AA4D-4CD1-9A2C-150E30D83510"),
            FirstName = "ORDER_FIRST_NAME",
            LastName = "ORDER_LAST_NAME",
            Email = "ORDER_EMAIL",
            LineOne = "ORDER_LINE_ONE",
            LineTwo = "ORDER_LINE_TWO",
            LineThree = "ORDER_LINE_THREE",
            Postcode = "ORDER_POSTCODE",
            City = "ORDER_CITY",
            County = "ORDER_COUNTY",
            Country = "ORDER_COUNTRY",
            Products = new List<ProductEntity>(),
        };
        var products = new List<ProductEntity>();
        var user = new UserModel
        {
            Id = Guid.Parse("CDCF7AAC-AA4D-4CD1-9A2C-150E30D83510"),
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
        
        var mockedOrderDataManager = new Mock<IOrderDataManager>();
        mockedOrderDataManager.Setup(x => x.GetOrder(Guid.Parse("AAAE3622-CB7A-4B7B-BFA4-CCCE40D7D162")))
            .ReturnsAsync(order);
        mockedOrderDataManager.Setup(x => x.UpdateOrder(updatedOrder, user)).ReturnsAsync(order);
        var mockedProductDataManager = new Mock<IProductDataManager>();
        mockedProductDataManager.Setup(x => x.GetProducts(order)).ReturnsAsync(products);

        var orderManager = new OrderManager(mockedOrderDataManager.Object,
            mockedProductDataManager.Object);

        var result = await orderManager.UpdateOrder(updatedOrder, user);
        
        Assert.Equal("ORDER_FIRST_NAME", result?.FirstName);
        Assert.Equal("ORDER_LAST_NAME", result?.LastName);
        Assert.Equal("ORDER_EMAIL", result?.Email);
        Assert.Equal("ORDER_LINE_ONE", result?.DeliveryAddress.LineOne);
        Assert.Equal("ORDER_LINE_TWO", result?.DeliveryAddress.LineTwo);
        Assert.Equal("ORDER_LINE_THREE", result?.DeliveryAddress.LineThree);
        Assert.Equal("ORDER_POSTCODE", result?.DeliveryAddress.Postcode);
        Assert.Equal("ORDER_CITY", result?.DeliveryAddress.City);
        Assert.Equal("ORDER_COUNTY", result?.DeliveryAddress.County);
        Assert.Equal("ORDER_COUNTRY", result?.DeliveryAddress.Country);
    }

    [Fact]
    public async void OrderManager_UpdateOrder_Unsuccessful()
    {
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
        var order = new OrderEntity
        {
            Id = Guid.Parse("AAAE3622-CB7A-4B7B-BFA4-CCCE40D7D162"),
            UserId = Guid.Parse("CDCF7AAC-AA4D-4CD1-9A2C-150E30D83510"),
            FirstName = "ORDER_FIRST_NAME",
            LastName = "ORDER_LAST_NAME",
            Email = "ORDER_EMAIL",
            LineOne = "ORDER_LINE_ONE",
            LineTwo = "ORDER_LINE_TWO",
            LineThree = "ORDER_LINE_THREE",
            Postcode = "ORDER_POSTCODE",
            City = "ORDER_CITY",
            County = "ORDER_COUNTY",
            Country = "ORDER_COUNTRY",
            Products = new List<ProductEntity>(),
        };
        var mappedOrder = new OrderModel()
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
        var products = new List<ProductEntity>();
        var user = new UserModel
        {
            Id = Guid.Parse("CDCF7AAC-AA4D-4CD1-9A2C-150E30D83510"),
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
        
        var mockedOrderDataManager = new Mock<IOrderDataManager>();
        mockedOrderDataManager.Setup(x => x.GetOrder(Guid.Parse("AAAE3622-CB7A-4B7B-BFA4-CCCE40D7D162")))
            .ReturnsAsync(order);
        mockedOrderDataManager.Setup(x => x.UpdateOrder(updatedOrder, user)).ReturnsAsync(order);
        var mockedProductDataManager = new Mock<IProductDataManager>();
        mockedProductDataManager.Setup(x => x.GetProducts(order)).ReturnsAsync(products);

        var orderManager = new OrderManager(mockedOrderDataManager.Object,
            mockedProductDataManager.Object);

        await Assert.ThrowsAsync<BadHttpRequestException>(() => orderManager.UpdateOrder(updatedOrder, user));
    }
}