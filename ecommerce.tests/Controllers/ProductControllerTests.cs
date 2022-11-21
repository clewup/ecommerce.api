using ecommerce.api.Classes;

namespace ecommerce.tests.Controllers;

public class ProductControllerTests
{
    [Fact]
    public void GetProducts_Successful()
    {
        var stock = new List<StockModel>()
        {
            new StockModel()
            {
                Variant = "VARIANT_1",
                Count = 123,
            },
            new StockModel()
            {
                Variant = "VARIANT_2",
                Count = 456,
            }
        };

        var products = new List<ProductModel>()
        {
            new ProductModel()
            {
                Id = new Guid(),
                Name = "PRODUCT1_NAME",
                Description = "PRODUCT1_DESC",
                Category = "PRODUCT1_CAT",
                Stock = stock,
                PricePerUnit = 123.45,
                IsDiscounted = false,
                Discount = null
            },
            new ProductModel()
            {
                Id = new Guid(),
                Name = "PRODUCT2_NAME",
                Description = "PRODUCT2_DESC",
                Category = "PRODUCT2_CAT",
                Stock = stock,
                PricePerUnit = 123.45,
                IsDiscounted = false,
                Discount = null
            },
            new ProductModel()
            {
                Id = new Guid(),
                Name = "PRODUCT3_NAME",
                Description = "PRODUCT3_DESC",
                Category = "PRODUCT3_CAT",
                Stock = stock,
                PricePerUnit = 123.45,
                IsDiscounted = false,
                Discount = null
            },
        };
    }
    
    [Fact]
    public void GetProductCategories_Successful()
    {

    }
    
    [Fact]
    public void GetProductVariants_Successful()
    {

    }
    
    [Fact]
    public void GetProduct_Successful()
    {
       
    }
    
    [Fact]
    public void CreateProduct_Successful()
    {
        
    }
    
    [Fact]
    public void UpdateProduct_Successful()
    {
        
    }
    
    [Fact]
    public void DeleteProduct_Successful()
    {
        
    }
}