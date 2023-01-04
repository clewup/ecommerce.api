using ecommerce.api.Data;
using ecommerce.api.DataManagers;
using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Entities;
using ecommerce.api.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ecommerce.tests.DataManagers;

public class ShippingDataManagerTests
{
    DbContextOptions<EcommerceDbContext> options = new DbContextOptionsBuilder<EcommerceDbContext>()
        .UseInMemoryDatabase(databaseName: "ShippingDataManagerTests")
        .Options;

    public ShippingDataManagerTests()
    {
        using (var context = new EcommerceDbContext(options))
        {
            context.Packages.AddAsync(new PackageEntity
            {
                Id = Guid.Parse("E4622CDA-41B1-4F2E-9BAA-A058297AE803"),
                TrackingNumber = Guid.Parse("B2A176F2-4AFD-4C77-AF2E-8385D7549FD7"),
                ShippedDate = DateTime.UtcNow.AddDays(-1),
                ArrivalDate = DateTime.UtcNow.AddDays(3),
                OrderId = Guid.Parse("D2745C33-3049-4625-847E-27ED94307764"),
            });
            context.Packages.AddAsync(new PackageEntity
            {
                Id = Guid.Parse("8461C523-14C1-43DC-8C49-2A01BDF55421"),
                TrackingNumber = Guid.Parse("4ED07A4E-EB57-4515-AD22-D3E460A682E0"),
                ShippedDate = DateTime.UtcNow.AddDays(-1),
                ArrivalDate = DateTime.UtcNow.AddDays(3),
                OrderId = Guid.Parse("8DE3808F-22BD-4815-8F55-22595C473A29"),
            });
            context.SaveChangesAsync();
        }
    }

    [Fact]
    public async void ShippingDataManager_TrackOrder_Successful()
    {
        var trackingNumber = Guid.Parse("4ED07A4E-EB57-4515-AD22-D3E460A682E0");
        var mockedOrderDataManager = new Mock<IOrderDataManager>();
        
        using (var context = new EcommerceDbContext(options))
        {
            var shippingDataManager = new ShippingDataManager(context, mockedOrderDataManager.Object);

            var result = await shippingDataManager.TrackOrder(trackingNumber);

            Assert.NotNull(result);
        }
    }
    
    [Fact]
    public async void ShippingDataManager_TrackOrder_Unsuccessful()
    {
        var trackingNumber = Guid.Parse("4EF07A4E-EB57-4515-AD22-D3E460A682E0");
        var mockedOrderDataManager = new Mock<IOrderDataManager>();
        
        using (var context = new EcommerceDbContext(options))
        {
            var shippingDataManager = new ShippingDataManager(context, mockedOrderDataManager.Object);

            var result = await shippingDataManager.TrackOrder(trackingNumber);

            Assert.Null(result);
        }
    }
    
    [Fact]
    public async void ShippingDataManager_ShipOrder_Successful()
    {
        var order = new OrderModel();
        var existingOrder = new OrderEntity();
        var user = new UserModel();
        var trackingNumber = Guid.Parse("13EC336F-DD0A-46DE-9163-1FBF8412E142");
        
        var mockedOrderDataManager = new Mock<IOrderDataManager>();
        mockedOrderDataManager.Setup(x => x.GetOrder(order.Id)).ReturnsAsync(existingOrder);
        mockedOrderDataManager.Setup(x => x.ShipOrder(order, user, trackingNumber)).ReturnsAsync(true);
        
        using (var context = new EcommerceDbContext(options))
        {
            var shippingDataManager = new ShippingDataManager(context, mockedOrderDataManager.Object);

            var result = await shippingDataManager.ShipOrder(order, user, trackingNumber);
            var entityResult = await shippingDataManager.TrackOrder(trackingNumber);

            Assert.True(result);
            Assert.NotNull(entityResult);
            Assert.Equal(DateTime.UtcNow.Date, entityResult?.ShippedDate.Date);
            Assert.Equal(DateTime.UtcNow.AddDays(4).Date, entityResult?.ArrivalDate.Date);
        }
    }
    
    [Fact]
    public async void ShippingDataManager_ShipOrder_Unsuccessful()
    {
        var order = new OrderModel();
        var existingOrder = new OrderEntity();
        var user = new UserModel();
        var trackingNumber = Guid.Parse("13EC336F-DD0A-46DE-9163-1FBF8412E142");
        
        var mockedOrderDataManager = new Mock<IOrderDataManager>();
        mockedOrderDataManager.Setup(x => x.ShipOrder(order, user, trackingNumber)).ReturnsAsync(false);
        
        using (var context = new EcommerceDbContext(options))
        {
            var shippingDataManager = new ShippingDataManager(context, mockedOrderDataManager.Object);

            var result = await shippingDataManager.ShipOrder(order, user, trackingNumber);

            Assert.False(result);
        }
    }
    
    [Fact]
    public async void ShippingDataManager_ExtendArrivalDate_Successful()
    {
        var trackingNumber = Guid.Parse("4ED07A4E-EB57-4515-AD22-D3E460A682E0");
        var user = new UserModel();
        var days = 1;
        
        var mockedOrderDataManager = new Mock<IOrderDataManager>();
        
        using (var context = new EcommerceDbContext(options))
        {
            var shippingDataManager = new ShippingDataManager(context, mockedOrderDataManager.Object);

            var result = await shippingDataManager.ExtendArrivalDate(trackingNumber, user, days);
            var entityResult = await shippingDataManager.TrackOrder(trackingNumber);
            
            Assert.True(result);
            Assert.Equal(DateTime.UtcNow.AddDays(4).Date, entityResult?.ArrivalDate.Date);
        }
    }
    
    [Fact]
    public async void ShippingDataManager_ExtendArrivalDate_Unsuccessful()
    {
        var orderId = Guid.Parse("D2745C33-3049-4625-847E-27ED94307763");
        var user = new UserModel();
        var days = 1;
        
        var mockedOrderDataManager = new Mock<IOrderDataManager>();
        
        using (var context = new EcommerceDbContext(options))
        {
            var shippingDataManager = new ShippingDataManager(context, mockedOrderDataManager.Object);

            var result = await shippingDataManager.ExtendArrivalDate(orderId, user, days);
            
            Assert.False(result);
        }
    }
}