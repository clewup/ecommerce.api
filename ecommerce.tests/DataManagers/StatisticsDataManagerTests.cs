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
                DateAdded = DateTime.UtcNow.AddDays(-2),
                CartId = Guid.Parse("D09DA6FD-5460-4FCA-A454-51640A896E11"),
                Cart = new CartEntity
                {
                    Id = Guid.Parse("D09DA6FD-5460-4FCA-A454-51640A896E11"),
                    UserId = Guid.Parse("C25F6176-F36A-4624-93A1-D84400517413"),
                    Total = 124.99,
                    Products = new List<ProductEntity>(),
                    Order = new OrderEntity(),
                },
                ProductId = Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA8"),
                Product = new ProductEntity()
                {
                    Id = Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA8"),
                    Name = "PRODUCT_1_NAME",
                    Description = "PRODUCT_1_DESCRIPTION",
                    Category = "PRODUCT_1_CATEGORY",
                    Range = "PRODUCT_1_RANGE",
                    Color = "PRODUCT_1_COLOR",
                    Stock = 0,
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
                }
            });
            context.CartProducts.Add(new CartProductEntity
            {
                DateAdded = DateTime.UtcNow.AddDays(-2),
                CartId = Guid.Parse("DAFA5DEF-C822-4EEF-B670-1465266C171E"),
                Cart = new CartEntity
                {
                    Id = Guid.Parse("DAFA5DEF-C822-4EEF-B670-1465266C171E"),
                    UserId = Guid.Parse("C25F6176-F36A-4624-93A1-D84400517413"),
                    Total = 124.99,
                    Products = new List<ProductEntity>(),
                    Order = new OrderEntity(),
                },
                ProductId = Guid.Parse("D08B30FB-EA25-4F6F-A386-4D247F5537FE"),
                Product = new ProductEntity()
                {
                    Id = Guid.Parse("D08B30FB-EA25-4F6F-A386-4D247F5537FE"),
                    Name = "PRODUCT_1_NAME",
                    Description = "PRODUCT_1_DESCRIPTION",
                    Category = "PRODUCT_1_CATEGORY",
                    Range = "PRODUCT_1_RANGE",
                    Color = "PRODUCT_1_COLOR",
                    Stock = 0,
                    Price = 30.00,
                    Discount = 0,
                    Images = new List<ImageEntity>()
                    {
                        new ImageEntity()
                        {
                            Title = "IMAGE_TITLE",
                            Description = "IMAGE_DESCRIPTION",
                            Url = new Uri("HTTP://IMAGE_URL.COM"),
                            ProductId = Guid.Parse("D08B30FB-EA25-4F6F-A386-4D247F5537FE"),
                        }
                    }
                }
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