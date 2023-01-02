using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Managers.Contracts;
using ecommerce.api.Models;

namespace ecommerce.api.Managers;

public class CategoriesManager : ICategoriesManager
{
    private readonly ICategoriesDataManager _categoriesDataManager;

    public CategoriesManager(ICategoriesDataManager categoriesDataManager)
    {
        _categoriesDataManager = categoriesDataManager;
    }
    
    public async Task<List<string>> GetCategories()
    {
        var categories = await _categoriesDataManager.GetCategories();
        return categories;
    }

    public async Task<List<string>> GetSubcategories()
    {
        var subcategories = await _categoriesDataManager.GetSubcategories();
        return subcategories;
    }
    
    public async Task<List<LinkedSubcategoriesModel>> GetLinkedSubcategories()
    {
        var subcategories = await _categoriesDataManager.GetLinkedSubcategories();
        return subcategories;
    }

    public async Task<List<string>> GetSubcategoriesByCategory(string category)
    {
        var subcategories = await _categoriesDataManager.GetSubcategoriesByCategory(category);
        return subcategories;
    }

    public async Task<List<string>> GetRanges()
    {
        var ranges = await _categoriesDataManager.GetRanges();
        return ranges;
    }
}