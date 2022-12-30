using AutoMapper;
using ecommerce.api.Classes;
using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Entities;
using ecommerce.api.Infrastructure;
using ecommerce.api.Managers;
using Moq;

namespace ecommerce.tests.Managers;

public class CartManagerTests
{
    [Fact]
    public async void CartManager_GetCarts_Successful()
    {
        var carts = new List<CartEntity>()
        {
            new CartEntity
            {
                Id = Guid.Parse("D7447BFA-6CBC-406F-8EE1-B85226C8081F"),
                UserId = Guid.Parse("1F9064A7-8FE6-4BAB-9EE6-37056FF731D3"),
                Total = 59.99,
                Products = new List<ProductEntity>(),
            },
            new CartEntity
            {
                Id = Guid.Parse("D7447BFA-6CBC-406F-8EE1-B85226C8081A"),
                UserId = Guid.Parse("1F9064A7-8FE6-4BAB-9EE6-37056FF731D2"),
                Total = 79.99,
                Products = new List<ProductEntity>(),
            },
        };
        var mappedCarts = new List<CartModel>()
        {
            new CartModel()
            {
                Id = Guid.Parse("D7447BFA-6CBC-406F-8EE1-B85226C8081F"),
                UserId = Guid.Parse("1F9064A7-8FE6-4BAB-9EE6-37056FF731D3"),
                Total = 59.99,
                Products = new List<ProductModel>(),
            },
            new CartModel()
            {
                Id = Guid.Parse("D7447BFA-6CBC-406F-8EE1-B85226C8081A"),
                UserId = Guid.Parse("1F9064A7-8FE6-4BAB-9EE6-37056FF731D2"),
                Total = 79.99,
                Products = new List<ProductModel>(),
            },
        };
        
        var mockedMapper = new Mock<IMapper>();
        mockedMapper.Setup(x => x.Map<List<CartModel>>(carts)).Returns(mappedCarts);
        var mockedCartDataManager = new Mock<ICartDataManager>();
        mockedCartDataManager.Setup(x => x.GetCarts()).ReturnsAsync(carts);

        var cartManager = new CartManager(mockedMapper.Object, mockedCartDataManager.Object);

        var result = await cartManager.GetCarts();
        
        Assert.Equal(2, result.Count);
    }
    
    [Fact]
    public async void CartManager_GetCart_Successful()
    {
        var cart = new CartEntity()
        {

            Id = Guid.Parse("D7447BFA-6CBC-406F-8EE1-B85226C8081F"),
            UserId = Guid.Parse("1F9064A7-8FE6-4BAB-9EE6-37056FF731D3"),
            Total = 59.99,
            Products = new List<ProductEntity>(),
        };
        var mappedCart = new CartModel()
        {
            Id = Guid.Parse("D7447BFA-6CBC-406F-8EE1-B85226C8081F"),
            UserId = Guid.Parse("1F9064A7-8FE6-4BAB-9EE6-37056FF731D3"),
            Total = 59.99,
            Products = new List<ProductModel>(),
        };
        
        var mockedMapper = new Mock<IMapper>();
        mockedMapper.Setup(x => x.Map<CartModel>(cart)).Returns(mappedCart);
        var mockedCartDataManager = new Mock<ICartDataManager>();
        mockedCartDataManager.Setup(x => x.GetCart(Guid.Parse("D7447BFA-6CBC-406F-8EE1-B85226C8081F"))).ReturnsAsync(cart);

        var cartManager = new CartManager(mockedMapper.Object, mockedCartDataManager.Object);

        var result = await cartManager.GetCart(Guid.Parse("D7447BFA-6CBC-406F-8EE1-B85226C8081F"));
        
        Assert.Equal(Guid.Parse("1F9064A7-8FE6-4BAB-9EE6-37056FF731D3"), result?.UserId);
        Assert.Equal(59.99, result?.Total);
    }
    
    [Fact]
    public async void CartManager_GetUserCart_Successful()
    {
        var cart = new CartEntity()
        {

            Id = Guid.Parse("D7447BFA-6CBC-406F-8EE1-B85226C8081F"),
            UserId = Guid.Parse("1F9064A7-8FE6-4BAB-9EE6-37056FF731D3"),
            Total = 59.99,
            Products = new List<ProductEntity>(),
        };
        var mappedCart = new CartModel()
        {
            Id = Guid.Parse("D7447BFA-6CBC-406F-8EE1-B85226C8081F"),
            UserId = Guid.Parse("1F9064A7-8FE6-4BAB-9EE6-37056FF731D3"),
            Total = 59.99,
            Products = new List<ProductModel>(),
        };
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

        var mockedMapper = new Mock<IMapper>();
        mockedMapper.Setup(x => x.Map<CartModel>(cart)).Returns(mappedCart);
        var mockedCartDataManager = new Mock<ICartDataManager>();
        mockedCartDataManager.Setup(x => x.GetUserCart(user)).ReturnsAsync(cart);

        var cartManager = new CartManager(mockedMapper.Object, mockedCartDataManager.Object);

        var result = await cartManager.GetUserCart(user);
        
        Assert.Equal(Guid.Parse("D7447BFA-6CBC-406F-8EE1-B85226C8081F"), result?.Id);
        Assert.Equal(59.99, result?.Total);
    }
    
    [Fact]
    public async void CartManager_CreateCart_Successful()
    {
        var createdCart = new CartModel()
        {
            Id = Guid.Parse("D7447BFA-6CBC-406F-8EE1-B85226C8081F"),
            UserId = Guid.Parse("1F9064A7-8FE6-4BAB-9EE6-37056FF731D3"),
            Total = 59.99,
            Products = new List<ProductModel>(),
        };
        var cart = new CartEntity()
        {

            Id = Guid.Parse("D7447BFA-6CBC-406F-8EE1-B85226C8081F"),
            UserId = Guid.Parse("1F9064A7-8FE6-4BAB-9EE6-37056FF731D3"),
            Total = 59.99,
            Products = new List<ProductEntity>(),
        };
        var mappedCart = new CartModel()
        {
            Id = Guid.Parse("D7447BFA-6CBC-406F-8EE1-B85226C8081F"),
            UserId = Guid.Parse("1F9064A7-8FE6-4BAB-9EE6-37056FF731D3"),
            Total = 59.99,
            Products = new List<ProductModel>(),
        };
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
        
        var mockedMapper = new Mock<IMapper>();
        mockedMapper.Setup(x => x.Map<CartModel>(cart)).Returns(mappedCart);
        var mockedCartDataManager = new Mock<ICartDataManager>();
        mockedCartDataManager.Setup(x => x.CreateCart(createdCart, user)).ReturnsAsync(cart);

        var cartManager = new CartManager(mockedMapper.Object, mockedCartDataManager.Object);

        var result = await cartManager.CreateCart(createdCart, user);
        
        Assert.Equal(Guid.Parse("1F9064A7-8FE6-4BAB-9EE6-37056FF731D3"), result?.UserId);
        Assert.Equal(59.99, result?.Total);
    }
    
    [Fact]
    public async void CartManager_UpdateCart_Successful()
    {
        var updatedCart = new CartModel()
        {
            Id = Guid.Parse("D7447BFA-6CBC-406F-8EE1-B85226C8081F"),
            UserId = Guid.Parse("1F9064A7-8FE6-4BAB-9EE6-37056FF731D3"),
            Total = 59.99,
            Products = new List<ProductModel>(),
        };
        var cart = new CartEntity()
        {

            Id = Guid.Parse("D7447BFA-6CBC-406F-8EE1-B85226C8081F"),
            UserId = Guid.Parse("1F9064A7-8FE6-4BAB-9EE6-37056FF731D3"),
            Total = 59.99,
            Products = new List<ProductEntity>(),
        };
        var mappedCart = new CartModel()
        {
            Id = Guid.Parse("D7447BFA-6CBC-406F-8EE1-B85226C8081F"),
            UserId = Guid.Parse("1F9064A7-8FE6-4BAB-9EE6-37056FF731D3"),
            Total = 59.99,
            Products = new List<ProductModel>(),
        };
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
        
        var mockedMapper = new Mock<IMapper>();
        mockedMapper.Setup(x => x.Map<CartModel>(cart)).Returns(mappedCart);
        var mockedCartDataManager = new Mock<ICartDataManager>();
        mockedCartDataManager.Setup(x => x.UpdateCart(updatedCart, user)).ReturnsAsync(cart);

        var cartManager = new CartManager(mockedMapper.Object, mockedCartDataManager.Object);

        var result = await cartManager.UpdateCart(updatedCart, user);
        
        Assert.Equal(Guid.Parse("1F9064A7-8FE6-4BAB-9EE6-37056FF731D3"), result?.UserId);
        Assert.Equal(59.99, result?.Total);
    }
}