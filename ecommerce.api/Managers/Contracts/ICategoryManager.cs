using ecommerce.api.Models;

namespace ecommerce.api.Managers.Contracts;

public interface ICategoryManager
{
    Task<List<string>> GetCategories();
    Task<List<string>> GetSubcategories();
    Task<List<LinkedSubcategoriesModel>> GetLinkedSubcategories();
    Task<List<string>> GetSubcategoriesByCategory(string category);
    Task<List<string>> GetRanges();
}