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
        var products = new List<ProductModel>();

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
        var products = new List<ProductModel>();

        var mockedLogger = new Mock<ILogger<StatisticsController>>();
        var mockedStatisticsManager = new Mock<IStatisticsManager>();
        mockedStatisticsManager.Setup(x => x.GetMostDiscountedProducts(2)).ReturnsAsync(products);

        var statisticsController = new StatisticsController(mockedLogger.Object, mockedStatisticsManager.Object);

        var result = await statisticsController.GetMostDiscountedProducts(2);
        
        Assert.IsType<OkObjectResult>(result);
    }
}