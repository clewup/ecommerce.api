using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Managers.Contracts;
using ecommerce.api.Models;

namespace ecommerce.api.Managers;

public class CategoryManager : ICategoryManager
{
    private readonly ICategoryDataManager _categoryDataManager;

    public CategoryManager(ICategoryDataManager categoryDataManager)
    {
        _categoryDataManager = categoryDataManager;
    }
    
    public async Task<List<string>> GetCategories()
    {
        var categories = await _categoryDataManager.GetCategories();
        return categories;
    }

    public async Task<List<string>> GetSubcategories()
    {
        var subcategories = await _categoryDataManager.GetSubcategories();
        return subcategories;
    }
    
    public async Task<List<LinkedSubcategoriesModel>> GetLinkedSubcategories()
    {
        var subcategories = await _categoryDataManager.GetLinkedSubcategories();
        return subcategories;
    }

    public async Task<List<string>> GetSubcategoriesByCategory(string category)
    {
        var subcategories = await _categoryDataManager.GetSubcategoriesByCategory(category);
        return subcategories;
    }

    public async Task<List<string>> GetRanges()
    {
        var ranges = await _categoryDataManager.GetRanges();
        return ranges;
    }
}