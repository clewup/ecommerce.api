using AutoMapper;
using ecommerce.api.Data;
using ecommerce.api.DataManagers;
using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Entities;
using ecommerce.api.Infrastructure;
using ecommerce.api.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ecommerce.tests.DataManagers;

public class CartDataManagerTests
{
    DbContextOptions<EcommerceDbContext> options = new DbContextOptionsBuilder<EcommerceDbContext>()
        .UseInMemoryDatabase(databaseName: "CartDataManagerTests")
        .Options;
    
    public CartDataManagerTests()
    {
        using (var context = new EcommerceDbContext(options))
        {
            context.Carts.Add(new CartEntity()
            {
                UserId = Guid.Parse("7211430F-7D6D-4371-8435-A30D4076594C"),
                Id = Guid.Parse("6BA2368C-5551-4503-A32F-6E2C7FBE3CB1"),
                Total = 30.00,
                Products = new List<ProductEntity>(),
            });
            context.Carts.Add(new CartEntity()
            {
                UserId = Guid.Parse("BA839B31-9FA9-41C0-A009-3AD3B1ADFB14"),
                Id = Guid.Parse("6BA2368C-5551-4503-A32F-6E2C7FBE3CB0"),
                Total = 60.00,
                Products = new List<ProductEntity>(),
            });
            context.SaveChangesAsync();
        }
    }
    
    [Fact]
    public async void CartDataManager_GetCarts_Successful()
    {
        var mockedProductDataManager = new Mock<IProductDataManager>();

        using (var context = new EcommerceDbContext(options))
        {
            var cartDataManager = new CartDataManager(context, mockedProductDataManager.Object);

            var result = await cartDataManager.GetCarts();
            
            Assert.Equal(3, result.Count());
        }
    }
    
    [Fact]
    public async void CartDataManager_GetCart_Successful()
    {
        var mockedProductDataManager = new Mock<IProductDataManager>();

        using (var context = new EcommerceDbContext(options))
        {
            var cartDataManager = new CartDataManager(context, mockedProductDataManager.Object);

            var result = await cartDataManager.GetCart(Guid.Parse("6BA2368C-5551-4503-A32F-6E2C7FBE3CB0"));

            Assert.Equal(60.00, result?.Total);
            Assert.Equal(2, result?.Products.Count());
        }
    }
    
    [Fact]
    public async void CartDataManager_GetUserCart_Successful()
    {
        var user = new UserModel()
        {
            Id = Guid.Parse("BA839B31-9FA9-41C0-A009-3AD3B1ADFB14"),
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
            Country = "USER_COUNTRY",
        };
        
        var mockedProductDataManager = new Mock<IProductDataManager>();

        using (var context = new EcommerceDbContext(options))
        {
            var cartDataManager = new CartDataManager(context, mockedProductDataManager.Object);

            var result = await cartDataManager.GetUserCart(user);
            
            Assert.Equal(60.00, result?.Total);
            Assert.Equal(2, result?.Products.Count());
        }
    }
    
    [Fact]
    public async void CartDataManager_CreateCart_Successful()
    {
        var cart = new CartModel()
        {
            UserId = Guid.Parse("ABFED34A-07F3-421E-BDE9-60B8E3D679A4"),
            Id = Guid.Parse("6BA2368C-5551-4503-A32F-6E2C7FBE3CB2"),
            Total = 30.00,
            Products = new List<ProductModel>(),
        };
        var mappedCart = new CartEntity()
        {
            UserId = Guid.Parse("ABFED34A-07F3-421E-BDE9-60B8E3D679A4"),
            Id = Guid.Parse("6BA2368C-5551-4503-A32F-6E2C7FBE3CB2"),
            Total = 30.00,
            Products = new List<ProductEntity>(),
        };
        var products = new List<ProductEntity>();
        var user = new UserModel()
        {
            Id = Guid.Parse("BA839B31-9FA9-41C0-A009-3AD3B1ADFB14"),
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
            Country = "USER_COUNTRY",
        };
        
        var mockedProductDataManager = new Mock<IProductDataManager>();
        mockedProductDataManager.Setup(x => x.GetProducts(mappedCart)).ReturnsAsync(products);

        using (var context = new EcommerceDbContext(options))
        {
            var cartDataManager = new CartDataManager(context, mockedProductDataManager.Object);

            var result = await cartDataManager.CreateCart(cart, user);
            
            Assert.Equal(30.00, result?.Total);
            Assert.Equal(1, result?.Products.Count());
        }
    }
    
    [Fact]
    public async void CartDataManager_UpdateCart_Successful()
    {
        var cart = new CartModel()
        {
            UserId = Guid.Parse("7211430F-7D6D-4371-8435-A30D4076594C"),
            Id = Guid.Parse("6BA2368C-5551-4503-A32F-6E2C7FBE3CB1"),
            Total = 30.00,
            Products = new List<ProductModel>(),
        };
        var mappedCart = new CartEntity()
        {
            UserId = Guid.Parse("7211430F-7D6D-4371-8435-A30D4076594C"),
            Id = Guid.Parse("6BA2368C-5551-4503-A32F-6E2C7FBE3CB1"),
            Total = 30.00,
            Products = new List<ProductEntity>(),
        };
        var products = new List<ProductEntity>();
        
        var user = new UserModel()
        {
            Id = Guid.Parse("7211430F-7D6D-4371-8435-A30D4076594C"),
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
            Country = "USER_COUNTRY",
        };
        
        var mockedProductDataManager = new Mock<IProductDataManager>();
        mockedProductDataManager.Setup(x => x.GetProducts(mappedCart)).ReturnsAsync(products);

        using (var context = new EcommerceDbContext(options))
        {
            var cartDataManager = new CartDataManager(context, mockedProductDataManager.Object);

            var result = await cartDataManager.UpdateCart(cart, user);
            
            Assert.Equal(0, result?.Total);
        }
    }
}