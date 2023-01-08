using AutoMapper;
using ecommerce.api.Data;
using ecommerce.api.DataManagers;
using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Entities;
using ecommerce.api.Infrastructure;
using ecommerce.api.Models;
using ecommerce.api.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ecommerce.tests.DataManagers;

public class OrderDataManagerTests
{
    DbContextOptions<EcommerceDbContext> options = new DbContextOptionsBuilder<EcommerceDbContext>()
        .UseInMemoryDatabase(databaseName: "OrderDataManagerTests")
        .EnableSensitiveDataLogging()
        .Options;
    
    public OrderDataManagerTests()
    {
        using (var context = new EcommerceDbContext(options))
        {
            context.Orders.Add(new OrderEntity
            {
                Id = Guid.Parse("04144215-C767-4497-910C-C10F5C5BE567"),
                UserId = Guid.Parse("CB75975F-9492-48C6-8BB3-5C86C62AE015"),
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
            });
            context.Orders.Add(new OrderEntity
            {
                Id = Guid.Parse("B8BBF159-354B-4CFB-BA7F-5B627C7D24BB"),
                UserId = Guid.Parse("C56E7364-C8F6-4685-8B3D-0301E71C0F68"),
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
            });
            context.SaveChangesAsync();
        }
    }

    [Fact]
    public async void OrderDataManager_GetOrders_Successful()
    {
        var mockedProductDataManager = new Mock<IProductDataManager>();
        var mockedCartDataManager = new Mock<ICartDataManager>();
        
        using (var context = new EcommerceDbContext(options))
        {
            var orderDataManager = new OrderDataManager(context, mockedProductDataManager.Object, mockedCartDataManager.Object);

            var result = await orderDataManager.GetOrders();

            Assert.Equal(3, result.Count);
        }
    }
    
    [Fact]
    public async void OrderDataManager_GetUserOrders_Successful()
    {
        var mockedProductDataManager = new Mock<IProductDataManager>();
        var mockedCartDataManager = new Mock<ICartDataManager>();

        using (var context = new EcommerceDbContext(options))
        {
            var user = new UserModel
            {
                Id = Guid.Parse("CB75975F-9492-48C6-8BB3-5C86C62AE015"),
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
            
            var orderDataManager = new OrderDataManager(context, mockedProductDataManager.Object, mockedCartDataManager.Object);

            var result = await orderDataManager.GetUserOrders(user);

            Assert.Single(result);
        }
    }
    
    [Fact]
    public async void OrderDataManager_GetOrder_Successful()
    {
        var mockedProductDataManager = new Mock<IProductDataManager>();
        var mockedCartDataManager = new Mock<ICartDataManager>();

        using (var context = new EcommerceDbContext(options))
        {
            var orderDataManager = new OrderDataManager(context, mockedProductDataManager.Object, mockedCartDataManager.Object);

            var result = await orderDataManager.GetOrder(Guid.Parse("04144215-C767-4497-910C-C10F5C5BE567"));

            Assert.Equal("ORDER_FIRST_NAME", result?.FirstName);
            Assert.Equal("ORDER_LAST_NAME", result?.LastName);
            Assert.Equal("ORDER_EMAIL", result?.Email);
            Assert.Equal("ORDER_LINE_ONE", result?.LineOne);
            Assert.Equal("ORDER_LINE_TWO", result?.LineTwo);
            Assert.Equal("ORDER_LINE_THREE", result?.LineThree);
            Assert.Equal("ORDER_POSTCODE", result?.Postcode);
            Assert.Equal("ORDER_CITY", result?.City);
            Assert.Equal("ORDER_COUNTY", result?.County);
            Assert.Equal("ORDER_COUNTRY", result?.Country);
        }
    }
    
    [Fact]
    public async void OrderDataManager_CreateOrder_Successful()
    {
        var order = new OrderModel
        {
            Id = Guid.Parse("2CC25D6C-A0DF-4902-8439-1F4ADABAF457"),
            UserId = Guid.Parse("A2BE8949-0B8F-42D5-836D-FD6F387A8696"),
            FirstName = "NEW_ORDER_FIRST_NAME",
            LastName = "NEW_ORDER_LAST_NAME",
            Email = "NEW_ORDER_EMAIL",
            DeliveryAddress = new AddressModel(),
            OrderDate = DateTime.UtcNow,
            Products = new List<ProductModel>()
            {
                new ProductModel()
                {
                    Id = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
                    Price = 30,
                    Images = new List<string>()
                    {
                        "https://www.fakeimage.com/image.jpg",
                        "https://www.fakeimage.com/image.jpg",
                    },
                    Name = "NAME",
                    Range = "RANGE",
                    Color = "BLACK",
                    Size = "SMALL"
                }
            }
        };
        var products = new List<ProductEntity>()
        {
            new ProductEntity()
            {
                Id = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
                Price = 30,
                Images = new List<ImageEntity>()
                {
                    new ImageEntity()
                    {
                        Url = new Uri("https://www.fakeimage.com/image.jpg"),
                        ProductId = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
                    },
                    new ImageEntity()
                    {
                        Url = new Uri("https://www.fakeimage.com/image.jpg"),
                        ProductId = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
                    }
                },
                Name = "PRODUCT NAME",
                Range = "PRODUCT_RANGE",
                Sku = "PRODUCT_RANGE-PN-SMALL-BLACK"
            },
        };
        var user = new UserModel();
        
        var mockedProductDataManager = new Mock<IProductDataManager>();
        mockedProductDataManager.Setup(x => x.GetProducts(It.IsAny<OrderEntity>())).ReturnsAsync(products);
        var mockedCartDataManager = new Mock<ICartDataManager>();
        
        using (var context = new EcommerceDbContext(options))
        {
            var orderDataManager = new OrderDataManager(context, mockedProductDataManager.Object, mockedCartDataManager.Object);

            var result = await orderDataManager.CreateOrder(order, user);
            
            Assert.Equal("NEW_ORDER_FIRST_NAME", result?.FirstName);
            Assert.Equal("NEW_ORDER_LAST_NAME", result?.LastName);
            Assert.Equal("NEW_ORDER_EMAIL", result?.Email);
        }
    }

    [Fact]
    public async void OrderDataManager_UpdateOrder_Successful()
    {
        var order = new OrderModel()
        {
            Id = Guid.Parse("B8BBF159-354B-4CFB-BA7F-5B627C7D24BB"),
            UserId = Guid.Parse("C56E7364-C8F6-4685-8B3D-0301E71C0F68"),
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
            Id = Guid.Parse("C56E7364-C8F6-4685-8B3D-0301E71C0F68"),
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
        var mockedCartDataManager = new Mock<ICartDataManager>();

        using (var context = new EcommerceDbContext(options))
        {
            var orderDataManager = new OrderDataManager(context, mockedProductDataManager.Object, mockedCartDataManager.Object);

            var result = orderDataManager.UpdateOrder(order, user);
            
            Assert.NotNull(result);
        }
    }
}