using AutoMapper;
using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Entities;
using ecommerce.api.Managers;
using ecommerce.api.Models;
using Moq;

namespace ecommerce.tests.Managers;

public class StatisticsManagerTests
{
    [Fact]
    public async void StatisticsManager_GetMostPopularProducts_Successful()
    {
        var cartProducts = new List<CartProductEntity>()
        {
            new CartProductEntity()
            {
                CartId = Guid.Parse("D09DA6FD-5460-4FCA-A454-51640A896E11"),
                Cart = new CartEntity
                {
                    Id = Guid.Parse("D09DA6FD-5460-4FCA-A454-51640A896E11"),
                    UserId = Guid.Parse("C25F6176-F36A-4624-93A1-D84400517413"),
                    Total = 124.99,
                    Products = new List<ProductEntity>(),
                },
                ProductId = Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA8"),
                Product = new ProductEntity()
            },
            new CartProductEntity()
            {
                CartId = Guid.Parse("DE48257E-116D-4DFD-85A5-0C837CC1333E"),
                Cart = new CartEntity
                {
                    Id = Guid.Parse("DE48257E-116D-4DFD-85A5-0C837CC1333E"),
                    UserId = Guid.Parse("C25F6176-F36A-4624-93A1-D84400517413"),
                    Total = 124.99,
                    Products = new List<ProductEntity>(),
                },
                ProductId = Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA8"),
                Product = new ProductEntity()
            },
            new CartProductEntity()
            {
                CartId = Guid.Parse("DAFA5DEF-C822-4EEF-B670-1465266C171E"),
                Cart = new CartEntity
                {
                    Id = Guid.Parse("DAFA5DEF-C822-4EEF-B670-1465266C171E"),
                    UserId = Guid.Parse("C25F6176-F36A-4624-93A1-D84400517413"),
                    Total = 124.99,
                    Products = new List<ProductEntity>(),
                },
                ProductId = Guid.Parse("D08B30FB-EA25-4F6F-A386-4D247F5537FE"),
                Product = new ProductEntity()
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
                Name = "PRODUCT 1 NAME",
                Range = "RANGE",
                Sku = "RANGE-P1N-SMALL-BLACK",
            },
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
                Name = "PRODUCT 2 NAME",
                Range = "RANGE",
                Sku = "RANGE-P2N-MEDIUM-BLACK",
            }
        };
        var productIds = new List<Guid>()
        {
            Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA8"),
            Guid.Parse("D08B30FB-EA25-4F6F-A386-4D247F5537FE"),
        };
        
        var mockedStatisticsDataManager = new Mock<IStatisticsDataManager>();
        mockedStatisticsDataManager.Setup(x => x.GetCartProducts()).ReturnsAsync(cartProducts);
        var mockedProductDataManager = new Mock<IProductDataManager>();
        mockedProductDataManager.Setup(x => x.GetProducts(productIds)).ReturnsAsync(products);

        var statisticsManager =
            new StatisticsManager(mockedStatisticsDataManager.Object, mockedProductDataManager.Object);

        var result = await statisticsManager.GetMostPopularProducts();
        
        Assert.Equal(2, result.Count);
    }
    
    [Fact]
    public async void StatisticsManager_GetMostDiscountedProducts_Successful()
    {
        var products = new List<ProductEntity>()
        {
            new ProductEntity()
            {
                Id = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
                Price = 30,
                Discount = new DiscountEntity()
                {
                    Id = Guid.Parse("1E2B87DB-3C1E-4AE3-9B2C-89697AE18543"),
                    Name = "DISCOUNT 2",
                    Percentage = 20,
                },
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
                Name = "PRODUCT 1 NAME",
                Range = "RANGE",
                Sku = "RANGE-P1N-SMALL-BLACK",
            },
            new ProductEntity()
            {
                Id = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
                Price = 30,
                Discount = new DiscountEntity()
                {
                    Id = Guid.Parse("1E2B87DB-3C1E-4AE3-9B2C-89697AE18543"),
                    Name = "DISCOUNT 2",
                    Percentage = 20,
                },
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
                Name = "PRODUCT 2 NAME",
                Range = "RANGE",
                Sku = "RANGE-P2N-MEDIUM-BLACK",
            }
        };
        
        var mockedStatisticsDataManager = new Mock<IStatisticsDataManager>();
        var mockedProductDataManager = new Mock<IProductDataManager>();
        mockedProductDataManager.Setup(x => x.GetProducts()).ReturnsAsync(products);

        var statisticsManager =
            new StatisticsManager(mockedStatisticsDataManager.Object, mockedProductDataManager.Object);

        var result = await statisticsManager.GetMostDiscountedProducts();
        
        Assert.Equal(2, result.Count);
        Assert.Equal(20, result.First().Discount?.Percentage);
    }
}