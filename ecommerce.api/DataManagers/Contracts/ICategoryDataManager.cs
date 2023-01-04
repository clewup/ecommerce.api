using ecommerce.api.Models;

namespace ecommerce.api.DataManagers.Contracts;

public interface ICategoryDataManager
{
    Task<List<string>> GetCategories();
    Task<List<string>> GetSubcategories();
    Task<List<LinkedSubcategoriesModel>> GetLinkedSubcategories();
    Task<List<string>> GetSubcategoriesByCategory(string category);
    Task<List<string>> GetRanges();
}