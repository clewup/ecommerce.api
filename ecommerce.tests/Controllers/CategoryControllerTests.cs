using Castle.Core.Logging;
using ecommerce.api.Controllers;
using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Managers;
using ecommerce.api.Managers.Contracts;
using ecommerce.api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace ecommerce.tests.Controllers;

public class CategoryControllerTests
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
        var mockedLogger = new Mock<ILogger<CategoryController>>();
        var mockedCategoryManager = new Mock<ICategoryManager>();
        mockedCategoryManager.Setup(x => x.GetCategories()).ReturnsAsync(categories);
        
        var categoryController = new CategoryController(mockedLogger.Object, mockedCategoryManager.Object);

        var result = await categoryController.GetCategories();
        
        Assert.IsType<OkObjectResult>(result);
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
        var mockedLogger = new Mock<ILogger<CategoryController>>();
        var mockedCategoryManager = new Mock<ICategoryManager>();
        mockedCategoryManager.Setup(x => x.GetSubcategories()).ReturnsAsync(subcategories);
        
        var categoryController = new CategoryController(mockedLogger.Object, mockedCategoryManager.Object);

        var result = await categoryController.GetSubcategories();
        
        Assert.IsType<OkObjectResult>(result);
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
        var mockedLogger = new Mock<ILogger<CategoryController>>();
        var mockedCategoryManager = new Mock<ICategoryManager>();
        mockedCategoryManager.Setup(x => x.GetLinkedSubcategories()).ReturnsAsync(subcategories);
        
        var categoryController = new CategoryController(mockedLogger.Object, mockedCategoryManager.Object);

        var result = await categoryController.GetLinkedSubcategories();
        
        Assert.IsType<OkObjectResult>(result);
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
        var mockedLogger = new Mock<ILogger<CategoryController>>();
        var mockedCategoryManager = new Mock<ICategoryManager>();
        mockedCategoryManager.Setup(x => x.GetSubcategoriesByCategory(category)).ReturnsAsync(subcategories);
        
        var categoryController = new CategoryController(mockedLogger.Object, mockedCategoryManager.Object);

        var result = await categoryController.GetSubcategoriesByCategory(category);
        
        Assert.IsType<OkObjectResult>(result);
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
        var mockedLogger = new Mock<ILogger<CategoryController>>();
        var mockedCategoryManager = new Mock<ICategoryManager>();
        mockedCategoryManager.Setup(x => x.GetRanges()).ReturnsAsync(ranges);
        
        var categoryController = new CategoryController(mockedLogger.Object, mockedCategoryManager.Object);

        var result = await categoryController.GetRanges();
        
        Assert.IsType<OkObjectResult>(result);
    }
}