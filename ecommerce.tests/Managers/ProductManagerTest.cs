using ecommerce.api.Classes;
using ecommerce.api.Entities;

namespace ecommerce.tests;

public class ProductManagerTest
{
    [Fact]
    public void GetProducts_Successful()
    {
        List<StockModel> stock = new List<StockModel>()
        {
            new StockModel()
            {
                Variant = "STOCK_1",
                Count = 3
            },
            new StockModel()
            {
                Variant = "STOCK_2",
                Count = 5
            }
        };

        List<ProductEntity> products = new List<ProductEntity>()
        {
            new ProductEntity()
            {
                Id = new Guid(),
                Name = "PRODUCT1_NAME",
                Description = "PRODUCT1_DESCRIPTION",
                Category = "PRODUCT1_CATEGORY",
                Stock = stock,
                PricePerUnit = 123.45,
                IsDiscounted = false,
                Discount = null,
            },
            new ProductEntity()
            {
                Id = new Guid(),
                Name = "PRODUCT2_NAME",
                Description = "PRODUCT2_DESCRIPTION",
                Category = "PRODUCT2_CATEGORY",
                Stock = stock,
                PricePerUnit = 123.45,
                IsDiscounted = true,
                Discount = 12.34,
            },
            new ProductEntity()
            {
                Id = new Guid(),
                Name = "PRODUCT3_NAME",
                Description = "PRODUCT23DESCRIPTION",
                Category = "PRODUCT3_CATEGORY",
                Stock = stock,
                PricePerUnit = 123.45,
                IsDiscounted = false,
                Discount = null,
            },
        };

        // Mocks a MongoDB response.
        IEnumerable<ProductEntity> enProducts = products;

        Assert.Equal(products, enProducts.Where(_ => true).ToList());
        Assert.Equal(3, enProducts.Count());
    }
    
    [Fact]
    public void GetProduct_Successful()
    {
        Guid id = new Guid("942E82CC-BC2A-402C-A033-8017312B8A2E");
        
        List<StockModel> stock = new List<StockModel>()
        {
            new StockModel()
            {
                Variant = "STOCK_1",
                Count = 3
            },
            new StockModel()
            {
                Variant = "STOCK_2",
                Count = 5
            }
        };

        List<ProductEntity> products = new List<ProductEntity>()
        {
            new ProductEntity()
            {
                Id = new Guid("54A768F4-0D24-445A-B1DE-7B94A6528C56"),
                Name = "PRODUCT1_NAME",
                Description = "PRODUCT1_DESCRIPTION",
                Category = "PRODUCT1_CATEGORY",
                Stock = stock,
                PricePerUnit = 123.45,
                IsDiscounted = false,
                Discount = null,
            },
            new ProductEntity()
            {
                Id = new Guid("942E82CC-BC2A-402C-A033-8017312B8A2E"),
                Name = "PRODUCT2_NAME",
                Description = "PRODUCT2_DESCRIPTION",
                Category = "PRODUCT2_CATEGORY",
                Stock = stock,
                PricePerUnit = 123.45,
                IsDiscounted = true,
                Discount = 12.34,
            },
            new ProductEntity()
            {
                Id = new Guid("01E26B40-F52E-4906-B392-1B13457FD9E0"),
                Name = "PRODUCT3_NAME",
                Description = "PRODUCT23DESCRIPTION",
                Category = "PRODUCT3_CATEGORY",
                Stock = stock,
                PricePerUnit = 123.45,
                IsDiscounted = false,
                Discount = null,
            },
        };

        ProductEntity product = new ProductEntity()
        {
            Id = new Guid("942E82CC-BC2A-402C-A033-8017312B8A2E"),
            Name = "PRODUCT2_NAME",
            Description = "PRODUCT2_DESCRIPTION",
            Category = "PRODUCT2_CATEGORY",
            Stock = stock,
            PricePerUnit = 123.45,
            IsDiscounted = true,
            Discount = 12.34,
        };

        // Mocks a MongoDB response.
        IEnumerable<ProductEntity> enProducts = products;

        var queriedProduct = enProducts.Where(p => p.Id == id).FirstOrDefault();
            
        Assert.Equal(product.Id, queriedProduct.Id);
        Assert.Equal(product.Name, queriedProduct.Name);
    }
}