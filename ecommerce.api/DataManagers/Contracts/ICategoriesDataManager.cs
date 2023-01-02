using ecommerce.api.Models;

namespace ecommerce.api.DataManagers.Contracts;

public interface ICategoriesDataManager
{
    Task<List<string>> GetCategories();
    Task<List<string>> GetSubcategories();
    Task<List<LinkedSubcategoriesModel>> GetLinkedSubcategories();
    Task<List<string>> GetSubcategoriesByCategory(string category);
    Task<List<string>> GetRanges();
}