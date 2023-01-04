using ecommerce.api.DataManagers;
using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Managers;
using ecommerce.api.Managers.Contracts;
using ecommerce.api.Models;
using Moq;

namespace ecommerce.tests.Managers;

public class CategoryManagerTests
{
    [Fact]
    public async void CategoryManager_GetCategories_Successful()
    {
        var categories = new List<string>()
        {
            "CATEGORY_1",
            "CATEGORY_2",
            "CATEGORY_3"
        };
        var mockedCategoryDataManager = new Mock<ICategoryDataManager>();
        mockedCategoryDataManager.Setup(x => x.GetCategories()).ReturnsAsync(categories);
        
        var categoryManager = new CategoryManager(mockedCategoryDataManager.Object);

        var result = await categoryManager.GetCategories();
        
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async void CategoryManager_GetSubcategories_Successful()
    {
        var subcategories = new List<string>()
        {
            "SUBCATEGORY_1",
            "SUBCATEGORY_2",
            "SUBCATEGORY_3"
        };
        var mockedCategoryDataManager = new Mock<ICategoryDataManager>();
        mockedCategoryDataManager.Setup(x => x.GetSubcategories()).ReturnsAsync(subcategories);
        
        var categoryManager = new CategoryManager(mockedCategoryDataManager.Object);
        
        var result = await categoryManager.GetSubcategories();
        
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async void CategoryManager_GetLinkedSubcategories_Successful()
    {
        var subcategories = new List<LinkedSubcategoriesModel>()
        {
            new LinkedSubcategoriesModel()
            {
                Category = "CATEGORY_1",
                Subcategories = new List<string>()
                {
                    "SUBCATEGORY_1"
                }
            },
            new LinkedSubcategoriesModel()
            {
                Category = "CATEGORY_2",
                Subcategories = new List<string>()
                {
                    "SUBCATEGORY_2"
                }
            },
            new LinkedSubcategoriesModel()
            {
                Category = "CATEGORY_3",
                Subcategories = new List<string>()
                {
                    "SUBCATEGORY_3"
                }
            },
        };
        var mockedCategoryDataManager = new Mock<ICategoryDataManager>();
        mockedCategoryDataManager.Setup(x => x.GetLinkedSubcategories()).ReturnsAsync(subcategories);
        
        var categoryManager = new CategoryManager(mockedCategoryDataManager.Object);
        
        var result = await categoryManager.GetLinkedSubcategories();
        
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async void CategoryManager_GetSubcategoriesByCategory_Successful()
    {
        var category = "CATEGORY";
        var subcategories = new List<string>()
        {
            "SUBCATEGORY_1",
            "SUBCATEGORY_2",
            "SUBCATEGORY_3"
        };
        var mockedCategoryDataManager = new Mock<ICategoryDataManager>();
        mockedCategoryDataManager.Setup(x => x.GetSubcategoriesByCategory(category)).ReturnsAsync(subcategories);
        
        var categoryManager = new CategoryManager(mockedCategoryDataManager.Object);
        
        var result = await categoryManager.GetSubcategoriesByCategory(category);
        
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async void CategoryManager_GetRanges_Successful()
    {
        var ranges = new List<string>()
        {
            "RANGE_1",
            "RANGE_2",
            "RANGE_3"
        };
        var mockedCategoryDataManager = new Mock<ICategoryDataManager>();
        mockedCategoryDataManager.Setup(x => x.GetRanges()).ReturnsAsync(ranges);
        
        var categoryManager = new CategoryManager(mockedCategoryDataManager.Object);
        
        var result = await categoryManager.GetRanges();
        
        Assert.Equal(3, result.Count());
    }
}