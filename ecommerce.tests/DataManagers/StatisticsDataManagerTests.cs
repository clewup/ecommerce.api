using ecommerce.api.Data;
using ecommerce.api.DataManagers;
using ecommerce.api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.tests.DataManagers;

public class StatisticsDataManagerTests
{
    DbContextOptions<EcommerceDbContext> options = new DbContextOptionsBuilder<EcommerceDbContext>()
        .UseInMemoryDatabase(databaseName: "StatisticsDataManagerTests")
        .Options;

    public StatisticsDataManagerTests()
    {
        using (var context = new EcommerceDbContext(options))
        {
            context.CartProducts.Add(new CartProductEntity
            {
                CartId = Guid.Parse("D09DA6FD-5460-4FCA-A454-51640A896E11"),
                Cart = new CartEntity(),
                ProductId = Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA8"),
                Product = new ProductEntity(),
            });
            context.CartProducts.Add(new CartProductEntity
            {
                CartId = Guid.Parse("DAFA5DEF-C822-4EEF-B670-1465266C171E"),
                Cart = new CartEntity(),
                ProductId = Guid.Parse("D08B30FB-EA25-4F6F-A386-4D247F5537FE"),
                Product = new ProductEntity(),
            });
            context.SaveChangesAsync();
        }
    }

    [Fact]
    public async void StatisticsDataManager_GetCartProducts_Successful()
    {
        using (var context = new EcommerceDbContext(options))
        {
            var statisticsDataManager = new StatisticsDataManager(context);

            var result = await statisticsDataManager.GetCartProducts();
            
            Assert.Equal(2, result.Count);
        }
    }
}