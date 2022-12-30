using AutoMapper;
using ecommerce.api.Classes;
using ecommerce.api.Data;
using ecommerce.api.DataManagers;
using ecommerce.api.Entities;
using ecommerce.api.Infrastructure;
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
                CartId = Guid.Parse("33EF268D-13B4-479B-8A9E-1AB355AF4494"),
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
                CartId = Guid.Parse("9641E936-51CA-4641-BA2A-923DA6F1DCD8"),
            });
            context.Carts.Add(new CartEntity()
            {
                UserId = Guid.Parse("A2BE8949-0B8F-42D5-836D-FD6F387A8696"),
                Id = Guid.Parse("E4266FFE-5E7B-43F1-B7CE-9581C11D638B"),
                Total = 60.00,
                Products = new List<ProductEntity>()
                {
                    new ProductEntity()
                    {
                        Id = Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA8"),
                        Name = "PRODUCT_NAME",
                        Description = "PRODUCT_DESCRIPTION",
                        Category = "PRODUCT_CATEGORY",
                        Range = "PRODUCT_RANGE",
                        Color = "PRODUCT_COLOR",
                        Stock = 10,
                        Price = 30.00,
                        Discount = 0,
                        Images = new List<ImageEntity>()
                        {
                            new ImageEntity()
                            {
                                Title = "IMAGE_TITLE",
                                Description = "IMAGE_DESCRIPTION",
                                Url = new Uri("HTTP://IMAGE_URL.COM"),
                                ProductId = Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA8"),
                            }
                        }
                    },
                    new ProductEntity()
                    {
                        Id = Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA6"),
                        Name = "PRODUCT_NAME",
                        Description = "PRODUCT_DESCRIPTION",
                        Category = "PRODUCT_CATEGORY",
                        Range = "PRODUCT_RANGE",
                        Color = "PRODUCT_COLOR",
                        Stock = 10,
                        Price = 30.00,
                        Discount = 0, 
                        Images = new List<ImageEntity>()
                        {
                            new ImageEntity()
                            {
                                Title = "IMAGE_TITLE",
                                Description = "IMAGE_DESCRIPTION",
                                Url = new Uri("HTTP://IMAGE_URL.COM"),
                                ProductId = Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA6"),
                            }
                        }
                    }
                }
            });
            context.Carts.Add(new CartEntity()
            {
                UserId = Guid.Parse("CB75975F-9492-48C6-8BB3-5C86C62AE015"),
                Id = Guid.Parse("9641E936-51CA-4641-BA2A-923DA6F1DCD8"),
                Total = 60.00,
                Products = new List<ProductEntity>()
                {
                    new ProductEntity()
                    {
                        Id = Guid.Parse("B184FE3C-36F2-4301-AEC8-D17B20AE8292"),
                        Name = "PRODUCT_NAME",
                        Description = "PRODUCT_DESCRIPTION",
                        Category = "PRODUCT_CATEGORY",
                        Range = "PRODUCT_RANGE",
                        Color = "PRODUCT_COLOR",
                        Stock = 10,
                        Price = 30.00,
                        Discount = 0,
                        Images = new List<ImageEntity>()
                        {
                            new ImageEntity()
                            {
                                Title = "IMAGE_TITLE",
                                Description = "IMAGE_DESCRIPTION",
                                Url = new Uri("HTTP://IMAGE_URL.COM"),
                                ProductId = Guid.Parse("B184FE3C-36F2-4301-AEC8-D17B20AE8292"),
                            }
                        }
                    },
                    new ProductEntity()
                    {
                        Id = Guid.Parse("99F24373-06B5-457C-B27C-4D4623317D8C"),
                        Name = "PRODUCT_NAME",
                        Description = "PRODUCT_DESCRIPTION",
                        Category = "PRODUCT_CATEGORY",
                        Range = "PRODUCT_RANGE",
                        Color = "PRODUCT_COLOR",
                        Stock = 10,
                        Price = 30.00,
                        Discount = 0, 
                        Images = new List<ImageEntity>()
                        {
                            new ImageEntity()
                            {
                                Title = "IMAGE_TITLE",
                                Description = "IMAGE_DESCRIPTION",
                                Url = new Uri("HTTP://IMAGE_URL.COM"),
                                ProductId = Guid.Parse("99F24373-06B5-457C-B27C-4D4623317D8C"),
                            }
                        }
                    }
                }
            });
            context.SaveChangesAsync();
        }
    }

    [Fact]
    public async void OrderDataManager_GetOrders_Successful()
    {
        var mockedMapper = new Mock<IMapper>();
        using (var context = new EcommerceDbContext(options))
        {
            var orderDataManager = new OrderDataManager(mockedMapper.Object, context);

            var result = await orderDataManager.GetOrders();

            Assert.IsType<List<OrderEntity>>(result);
            Assert.Equal(3, result.Count);
        }
    }
    
    [Fact]
    public async void OrderDataManager_GetUserOrders_Successful()
    {
        var mockedMapper = new Mock<IMapper>();
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
            
            var orderDataManager = new OrderDataManager(mockedMapper.Object, context);

            var result = await orderDataManager.GetUserOrders(user);

            Assert.IsType<List<OrderEntity>>(result);
            Assert.Single(result);
        }
    }
    
    [Fact]
    public async void OrderDataManager_GetOrder_Successful()
    {
        var mockedMapper = new Mock<IMapper>();
        using (var context = new EcommerceDbContext(options))
        {
            var orderDataManager = new OrderDataManager(mockedMapper.Object, context);

            var result = await orderDataManager.GetOrder(Guid.Parse("04144215-C767-4497-910C-C10F5C5BE567"));

            Assert.IsType<OrderEntity>(result);
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
            Assert.Equal(Guid.Parse("33EF268D-13B4-479B-8A9E-1AB355AF4494"), result?.CartId);
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
            Cart = new CartModel()
            {
                Id = Guid.Parse("E4266FFE-5E7B-43F1-B7CE-9581C11D638B"),
                UserId = Guid.Parse("A2BE8949-0B8F-42D5-836D-FD6F387A8696"),
            },
            OrderDate = DateTime.UtcNow,
        };
        var mappedOrder = new OrderEntity
        {
            Id = Guid.Parse("2CC25D6C-A0DF-4902-8439-1F4ADABAF457"),
            UserId = Guid.Parse("A2BE8949-0B8F-42D5-836D-FD6F387A8696"),
            FirstName = "NEW_ORDER_FIRST_NAME",
            LastName = "NEW_ORDER_LAST_NAME",
            Email = "NEW_ORDER_EMAIL",
            LineOne = "NEW_ORDER_LINE_ONE",
            LineTwo = "NEW_ORDER_LINE_TWO",
            LineThree = "NEW_ORDER_LINE_THREE",
            Postcode = "NEW_ORDER_POSTCODE",
            City = "NEW_ORDER_CITY",
            County = "NEW_ORDER_COUNTY",
            Country = "NEW_ORDER_COUNTRY",
            CartId = Guid.Parse("E4266FFE-5E7B-43F1-B7CE-9581C11D638B"),
        };
        var user = new UserModel
        {
            Id = Guid.Parse("A2BE8949-0B8F-42D5-836D-FD6F387A8696"),
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
        mockedMapper.Setup(x => x.Map<OrderEntity>(order)).Returns(mappedOrder);
        
        using (var context = new EcommerceDbContext(options))
        {
            var orderDataManager = new OrderDataManager(mockedMapper.Object, context);

            var result = await orderDataManager.CreateOrder(order, user);
            
            Assert.NotNull(result);
            Assert.IsType<OrderEntity>(result);
            Assert.Equal("NEW_ORDER_FIRST_NAME", result?.FirstName);
            Assert.Equal("NEW_ORDER_LAST_NAME", result?.LastName);
            Assert.Equal("NEW_ORDER_EMAIL", result?.Email);
            Assert.Equal("NEW_ORDER_LINE_ONE", result?.LineOne);
            Assert.Equal("NEW_ORDER_LINE_TWO", result?.LineTwo);
            Assert.Equal("NEW_ORDER_LINE_THREE", result?.LineThree);
            Assert.Equal("NEW_ORDER_POSTCODE", result?.Postcode);
            Assert.Equal("NEW_ORDER_CITY", result?.City);
            Assert.Equal("NEW_ORDER_COUNTY", result?.County);
            Assert.Equal("NEW_ORDER_COUNTRY", result?.Country);
            Assert.Equal(Guid.Parse("E4266FFE-5E7B-43F1-B7CE-9581C11D638B"), result?.CartId);
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
        
        var mockedMapper = new Mock<IMapper>();
        using (var context = new EcommerceDbContext(options))
        {
            var orderDataManager = new OrderDataManager(mockedMapper.Object, context);

            var result = orderDataManager.UpdateOrder(order, user);
            
            Assert.NotNull(result);
        }
    }
}