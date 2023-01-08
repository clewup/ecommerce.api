using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Entities;
using ecommerce.api.Managers;
using ecommerce.api.Models;
using Moq;

namespace ecommerce.tests.Managers;

public class ShippingManagerTests
{
    [Fact]
    public async void ShippingManager_TrackOrder_Successful()
    {
        var trackingNumber = Guid.Parse("E9C00F5A-EE5E-4FD0-B59B-0FFA65053EEA");
        var package = new PackageEntity()
        {
            Id = trackingNumber,
            Order = new OrderEntity()
            {
                Products = new List<ProductEntity>(),
            },
        };
        
        var mockedShippingDataManager = new Mock<IShippingDataManager>();
        mockedShippingDataManager.Setup(x => x.TrackOrder(trackingNumber)).ReturnsAsync(package);
        
        var shippingManager = new ShippingManager(mockedShippingDataManager.Object);

        var result = await shippingManager.TrackOrder(trackingNumber);
        
        Assert.NotNull(result);
    }
    
    [Fact]
    public async void ShippingManager_ShipOrder_Successful()
    {
        var order = new OrderModel();
        var user = new UserModel();
        var package = new PackageEntity()
        {
            Id = Guid.Parse("DF3DCD85-0A4B-49BD-A67F-F9E1D45F0A74"),
            Order = new OrderEntity()
            {
                Products = new List<ProductEntity>(),
            },
        };
        
        var mockedShippingDataManager = new Mock<IShippingDataManager>();
        mockedShippingDataManager.Setup(x => x.ShipOrder(order, user)).ReturnsAsync(package);
        
        var shippingManager = new ShippingManager(mockedShippingDataManager.Object);

        var result = await shippingManager.ShipOrder(order, user);
        
        Assert.NotNull(result);
    }
    
    [Fact]
    public async void ShippingManager_ExtendArrivalDate_Successful()
    {
        var trackingNumber = Guid.Parse("DF3DCD85-0A4B-49BD-A67F-F9E1D45F0A74");
        var user = new UserModel();
        var package = new PackageEntity()
        {
            Id = trackingNumber,
            ArrivalDate = DateTime.UtcNow.AddDays(3),
            Order = new OrderEntity()
            {
                Products = new List<ProductEntity>(),
            },
        };
        
        var mockedShippingDataManager = new Mock<IShippingDataManager>();
        mockedShippingDataManager.Setup(x => x.ExtendArrivalDate(trackingNumber, user, 3)).ReturnsAsync(package);
        
        var shippingManager = new ShippingManager(mockedShippingDataManager.Object);

        var result = await shippingManager.ExtendArrivalDate(trackingNumber, user, 3);
        
        Assert.NotNull(result);
    }
}