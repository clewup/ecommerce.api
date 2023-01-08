using ecommerce.api.Controllers;
using ecommerce.api.Entities;
using ecommerce.api.Managers.Contracts;
using ecommerce.api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace ecommerce.tests.Controllers;

public class ShippingControllerTests
{
    [Fact]
    public async void ShippingController_TrackOrder_Successful()
    {
        var package = new PackageModel();
        var trackingNumber = Guid.Parse("80D67D37-7D88-4993-9637-C1F4929E0186");
        
        var mockedLogger = new Mock<ILogger<ShippingController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        var mockedShippingManager = new Mock<IShippingManager>();
        mockedShippingManager.Setup(x => x.TrackOrder(trackingNumber)).ReturnsAsync(package);
        
        var shippingController = new ShippingController(mockedLogger.Object, mockedClaimsManager.Object, mockedShippingManager.Object);
        
        var result = await shippingController.TrackOrder(trackingNumber);
        
        Assert.IsType<OkObjectResult>(result);
    }
    
    [Fact]
    public async void ShippingController_TrackOrder_Unsuccessful()
    {
        var trackingNumber = Guid.Parse("80D67D37-7D88-4993-9637-C1F4929E0186");
        
        var mockedLogger = new Mock<ILogger<ShippingController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        var mockedShippingManager = new Mock<IShippingManager>();
        
        var shippingController = new ShippingController(mockedLogger.Object, mockedClaimsManager.Object, mockedShippingManager.Object);
        
        var result = await shippingController.TrackOrder(trackingNumber);
        
        Assert.IsType<BadRequestObjectResult>(result);
    }
    
    [Fact]
    public async void ShippingController_ShipOrder_Successful()
    {
        var order = new OrderModel();
        var package = new PackageModel();

        var mockedLogger = new Mock<ILogger<ShippingController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        var mockedShippingManager = new Mock<IShippingManager>();
        mockedShippingManager.Setup(x => x.ShipOrder(order, It.IsAny<UserModel>())).ReturnsAsync(package);
        
        var shippingController = new ShippingController(mockedLogger.Object, mockedClaimsManager.Object, mockedShippingManager.Object);

        var result = await shippingController.ShipOrder(order);
        
        Assert.IsType<OkObjectResult>(result);
    }
    
    [Fact]
    public async void ShippingController_ShipOrder_Unsuccessful()
    {
        var order = new OrderModel();
        
        var mockedLogger = new Mock<ILogger<ShippingController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        var mockedShippingManager = new Mock<IShippingManager>();
        
        var shippingController = new ShippingController(mockedLogger.Object, mockedClaimsManager.Object, mockedShippingManager.Object);

        var result = await shippingController.ShipOrder(order);
        
        Assert.IsType<BadRequestObjectResult>(result);
    }
    
    [Fact]
    public async void ShippingController_ExtendArrivalDate_Successful()
    {
        var trackingNumber = Guid.Parse("31092EF0-29EA-4E61-BBED-A098FB11ED1B");
        var days = 3;
        var package = new PackageModel();
        
        var mockedLogger = new Mock<ILogger<ShippingController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        var mockedShippingManager = new Mock<IShippingManager>();
        mockedShippingManager.Setup(x => x.ExtendArrivalDate(trackingNumber, It.IsAny<UserModel>(), days))
            .ReturnsAsync(package);
        
        var shippingController = new ShippingController(mockedLogger.Object, mockedClaimsManager.Object, mockedShippingManager.Object);

        var result = await shippingController.ExtendArrivalDate(trackingNumber, days);
        
        Assert.IsType<OkObjectResult>(result);
    }
    
    [Fact]
    public async void ShippingController_ExtendArrivalDate_Unsuccessful()
    {
        var trackingNumber = Guid.Parse("31092EF0-29EA-4E61-BBED-A098FB11ED1B");
        var days = 3;
        
        var mockedLogger = new Mock<ILogger<ShippingController>>();
        var mockedClaimsManager = new Mock<IClaimsManager>();
        var mockedShippingManager = new Mock<IShippingManager>();
        
        var shippingController = new ShippingController(mockedLogger.Object, mockedClaimsManager.Object, mockedShippingManager.Object);

        var result = await shippingController.ExtendArrivalDate(trackingNumber, days);
        
        Assert.IsType<BadRequestObjectResult>(result);
    }
}