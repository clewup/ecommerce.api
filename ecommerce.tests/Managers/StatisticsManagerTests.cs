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
        var products = new List<ProductEntity>();
        var mappedProducts = new List<ProductModel>();
        var productIds = new List<Guid>()
        {
            Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA8"),
            Guid.Parse("D08B30FB-EA25-4F6F-A386-4D247F5537FE"),
        };
        
        var mockedMapper = new Mock<IMapper>();
        mockedMapper.Setup(x => x.Map<List<ProductModel>>(products)).Returns(mappedProducts);
        var mockedStatisticsDataManager = new Mock<IStatisticsDataManager>();
        mockedStatisticsDataManager.Setup(x => x.GetCartProducts()).ReturnsAsync(cartProducts);
        var mockedProductDataManager = new Mock<IProductDataManager>();
        mockedProductDataManager.Setup(x => x.GetProducts(productIds)).ReturnsAsync(products);

        var statisticsManager =
            new StatisticsManager(mockedMapper.Object, mockedStatisticsDataManager.Object, mockedProductDataManager.Object);

        var result = await statisticsManager.GetMostPopularProducts();
        
        Assert.Equal(2, result.Count);
        Assert.Equal("PRODUCT_1_NAME", result.First().Name);
    }
    
    [Fact]
    public async void StatisticsManager_GetMostDiscountedProducts_Successful()
    {
        var products = new List<ProductEntity>();
        var mappedProducts = new List<ProductModel>();
        
        var mockedMapper = new Mock<IMapper>();
        mockedMapper.Setup(x => x.Map<List<ProductModel>>(products)).Returns(mappedProducts);
        var mockedStatisticsDataManager = new Mock<IStatisticsDataManager>();
        var mockedProductDataManager = new Mock<IProductDataManager>();
        mockedProductDataManager.Setup(x => x.GetProducts()).ReturnsAsync(products);

        var statisticsManager =
            new StatisticsManager(mockedMapper.Object, mockedStatisticsDataManager.Object, mockedProductDataManager.Object);

        var result = await statisticsManager.GetMostDiscountedProducts();
        
        Assert.Equal(2, result.Count);
        Assert.Equal("PRODUCT_1_NAME", result.First().Name);
    }
}