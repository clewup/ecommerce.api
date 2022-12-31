using ecommerce.api.Controllers;
using ecommerce.api.Managers.Contracts;
using ecommerce.api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace ecommerce.tests.Controllers;

public class StatisticsControllerTests
{
    [Fact]
    public async void StatisticsController_GetMostPopularProducts_Successful()
    {
        var products = new List<ProductModel>()
        {
            new ProductModel()
            {
                Id = Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA4"),
                Name = "PRODUCT_1_NAME",
                Description = "PRODUCT_1_DESCRIPTION",
                Category = "PRODUCT_1_CATEGORY",
                Range = "PRODUCT_1_RANGE",
                Color = "PRODUCT_1_COLOR",
                Stock = 10,
                Price = 30.00,
                Discount = 0,
                Images = new List<string>()
                {
                    "HTTP://IMAGE_URL.COM"
                }
            },
            new ProductModel()
            {
                Id = Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AB6"),
                Name = "PRODUCT_2_NAME",
                Description = "PRODUCT_2_DESCRIPTION",
                Category = "PRODUCT_2_CATEGORY",
                Range = "PRODUCT_2_RANGE",
                Color = "PRODUCT_2_COLOR",
                Stock = 10,
                Price = 30.00,
                Discount = 0, 
                Images = new List<string>()
                {
                    "HTTP://IMAGE_URL.COM"
                }
            }
        };

        var mockedLogger = new Mock<ILogger<StatisticsController>>();
        var mockedStatisticsManager = new Mock<IStatisticsManager>();
        mockedStatisticsManager.Setup(x => x.GetMostPopularProducts(2)).ReturnsAsync(products);

        var statisticsController = new StatisticsController(mockedLogger.Object, mockedStatisticsManager.Object);

        var result = await statisticsController.GetMostPopularProducts(2);
        
        Assert.IsType<OkObjectResult>(result);
    }
    
    [Fact]
    public async void StatisticsController_GetMostDiscountedProducts_Successful()
    {
        var products = new List<ProductModel>()
        {
            new ProductModel()
            {
                Id = Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA4"),
                Name = "PRODUCT_1_NAME",
                Description = "PRODUCT_1_DESCRIPTION",
                Category = "PRODUCT_1_CATEGORY",
                Range = "PRODUCT_1_RANGE",
                Color = "PRODUCT_1_COLOR",
                Stock = 10,
                Price = 30.00,
                Discount = 0,
                Images = new List<string>()
                {
                    "HTTP://IMAGE_URL.COM"
                }
            },
            new ProductModel()
            {
                Id = Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AB6"),
                Name = "PRODUCT_2_NAME",
                Description = "PRODUCT_2_DESCRIPTION",
                Category = "PRODUCT_2_CATEGORY",
                Range = "PRODUCT_2_RANGE",
                Color = "PRODUCT_2_COLOR",
                Stock = 10,
                Price = 30.00,
                Discount = 0, 
                Images = new List<string>()
                {
                    "HTTP://IMAGE_URL.COM"
                }
            }
        };

        var mockedLogger = new Mock<ILogger<StatisticsController>>();
        var mockedStatisticsManager = new Mock<IStatisticsManager>();
        mockedStatisticsManager.Setup(x => x.GetMostDiscountedProducts(2)).ReturnsAsync(products);

        var statisticsController = new StatisticsController(mockedLogger.Object, mockedStatisticsManager.Object);

        var result = await statisticsController.GetMostDiscountedProducts(2);
        
        Assert.IsType<OkObjectResult>(result);
    }
}