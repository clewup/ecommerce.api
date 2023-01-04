using ecommerce.api.Data;
using ecommerce.api.DataManagers;
using ecommerce.api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.tests.DataManagers;

public class CategoryDataManagerTests
{
    DbContextOptions<EcommerceDbContext> options = new DbContextOptionsBuilder<EcommerceDbContext>()
        .UseInMemoryDatabase(databaseName: "CategoryDataManagerTests")
        .Options;

    public CategoryDataManagerTests()
    {
        using (var context = new EcommerceDbContext(options))
        {
            context.Products.AddAsync(new ProductEntity()
            {
                Category = "CATEGORY_1",
                Subcategory = "SUBCATEGORY_1",
                Range = "RANGE"
            });
            context.Products.AddAsync(new ProductEntity()
            {
                Category = "CATEGORY_2",
                Subcategory = "SUBCATEGORY_2",
                Range = "RANGE"
            });
            context.Products.AddAsync(new ProductEntity()
            {
                Category = "CATEGORY_3",
                Subcategory = "SUBCATEGORY_3",
                Range = "RANGE"
            });
            context.SaveChangesAsync();
        }
    }

    [Fact]
    public async void CategoryDataManager_GetCategories_Successful()
    {
        using (var context = new EcommerceDbContext(options))
        {
            var categoryDataManager = new CategoryDataManager(context);

            var result = await categoryDataManager.GetCategories();
            
            Assert.Equal(3, result.Count());
        }
    }
    
    [Fact]
    public async void CategoryDataManager_GetSubcategories_Successful()
    {
        using (var context = new EcommerceDbContext(options))
        {
            var categoryDataManager = new CategoryDataManager(context);

            var result = await categoryDataManager.GetSubcategories();
            
            Assert.Equal(3, result.Count());
        }
    }
    
    [Fact]
    public async void CategoryDataManager_GetLinkedSubcategories_Successful()
    {
        using (var context = new EcommerceDbContext(options))
        {
            var categoryDataManager = new CategoryDataManager(context);

            var result = await categoryDataManager.GetLinkedSubcategories();
            
            Assert.Equal(3, result.Count());
        }
    }
    
    [Fact]
    public async void CategoryDataManager_GetSubcategoriesByCategory_Successful()
    {
        var category = "CATEGORY_1";
        
        using (var context = new EcommerceDbContext(options))
        {
            var categoryDataManager = new CategoryDataManager(context);

            var result = await categoryDataManager.GetSubcategoriesByCategory(category);
            
            Assert.Single(result);
        }
    }
    
    [Fact]
    public async void CategoryDataManager_GetRanges_Successful()
    {
        using (var context = new EcommerceDbContext(options))
        {
            var categoryDataManager = new CategoryDataManager(context);

            var result = await categoryDataManager.GetRanges();
            
            Assert.Single(result);
        }
    }
}