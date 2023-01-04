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

            var result = await cartDataManager.GetCart(Guid.Parse("6BA2368C-5551-4503-A32F-6E2C7FBE3CB1"));

            Assert.Equal(30.00, result?.Total);
        }
    }
    
    [Fact]
    public async void CartDataManager_GetUserCart_Successful()
    {
        var user = new UserModel()
        {
            Id = Guid.Parse("BA839B31-9FA9-41C0-A009-3AD3B1ADFB14"),
        };
        
        var mockedProductDataManager = new Mock<IProductDataManager>();

        using (var context = new EcommerceDbContext(options))
        {
            var cartDataManager = new CartDataManager(context, mockedProductDataManager.Object);

            var result = await cartDataManager.GetUserCart(user);
            
            Assert.Equal(60.00, result?.Total);
        }
    }
    
    [Fact]
    public async void CartDataManager_CreateCart_Successful()
    {
        var cart = new CartModel
        {
            UserId = Guid.Parse("ABFED34A-07F3-421E-BDE9-60B8E3D679A4"),
            Id = Guid.Parse("6BA2368C-5551-4503-A32F-6E2C7FBE3CB2"),
            Total = 30.00,
            Products = new List<ProductModel>()
            {
                new ProductModel
                {
                    Id = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
                    Price = 30,
                    Images = new List<string>()
                    {
                        "https://www.fakeimage.com/image.jpg",
                        "https://www.fakeimage.com/image.jpg",
                    },
                    Sizes = new List<SizeModel>()
                    {
                        new SizeModel()
                        {
                            Size = SizeType.XSmall,
                            Stock = 10,
                        },
                        new SizeModel()
                        {
                            Size = SizeType.Small,
                            Stock = 10,
                        },
                        new SizeModel()
                        {
                            Size = SizeType.Medium,
                            Stock = 10,
                        },
                        new SizeModel()
                        {
                            Size = SizeType.Large,
                            Stock = 10,
                        },
                        new SizeModel()
                        {
                            Size = SizeType.XLarge,
                            Stock = 10,
                        },
                    },
                },
            }
        };
        var products = new List<ProductEntity>();
        var user = new UserModel();
        
        var mockedProductDataManager = new Mock<IProductDataManager>();
        mockedProductDataManager.Setup(x => x.GetProducts(It.IsAny<CartEntity>())).ReturnsAsync(products);

        using (var context = new EcommerceDbContext(options))
        {
            var cartDataManager = new CartDataManager(context, mockedProductDataManager.Object);

            var result = await cartDataManager.CreateCart(cart, user);
            
            Assert.NotNull(result);
        }
    }
    
    [Fact]
    public async void CartDataManager_UpdateCart_Successful()
    {
        var cart = new CartModel
        {
            UserId = Guid.Parse("7211430F-7D6D-4371-8435-A30D4076594C"),
            Id = Guid.Parse("6BA2368C-5551-4503-A32F-6E2C7FBE3CB1"),
            Total = 30.00,
            Products = new List<ProductModel>()
            {
                new ProductModel
                {
                    Id = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
                    Price = 30,
                    Images = new List<string>()
                    {
                        "https://www.fakeimage.com/image.jpg",
                        "https://www.fakeimage.com/image.jpg",
                    },
                    Sizes = new List<SizeModel>()
                    {
                        new SizeModel()
                        {
                            Size = SizeType.XSmall,
                            Stock = 10,
                        },
                        new SizeModel()
                        {
                            Size = SizeType.Small,
                            Stock = 10,
                        },
                        new SizeModel()
                        {
                            Size = SizeType.Medium,
                            Stock = 10,
                        },
                        new SizeModel()
                        {
                            Size = SizeType.Large,
                            Stock = 10,
                        },
                        new SizeModel()
                        {
                            Size = SizeType.XLarge,
                            Stock = 10,
                        },
                    },
                },
            }
        };
        var products = new List<ProductEntity>();
        var user = new UserModel();
        
        var mockedProductDataManager = new Mock<IProductDataManager>();
        mockedProductDataManager.Setup(x => x.GetProducts(It.IsAny<CartEntity>())).ReturnsAsync(products);

        using (var context = new EcommerceDbContext(options))
        {
            var cartDataManager = new CartDataManager(context, mockedProductDataManager.Object);

            var result = await cartDataManager.UpdateCart(cart, user);
            
            Assert.NotNull(result);
        }
    }
}