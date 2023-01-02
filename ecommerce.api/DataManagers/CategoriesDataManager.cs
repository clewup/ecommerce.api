using ecommerce.api.Data;
using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.api.DataManagers;

public class CategoriesDataManager : ICategoriesDataManager
{
    private readonly EcommerceDbContext _context;

    public CategoriesDataManager(EcommerceDbContext context)
    {
        _context = context;
    }

    public async Task<List<string>> GetCategories()
    {
        var categories = await _context.Products.Select(x => x.Category).Distinct().ToListAsync();

        return categories;
    }
    
    public async Task<List<string>> GetSubcategories()
    {
        var subcategories = await _context.Products.Select(x => x.Subcategory).Distinct().ToListAsync();

        return subcategories;
    }
    
    public async Task<List<LinkedSubcategoriesModel>> GetLinkedSubcategories()
    {
        var categories = await GetCategories();
        var subcategories = new List<LinkedSubcategoriesModel>();

        foreach (var category in categories)
        {
            var categorySubcategories = await _context.Products.Where(x => x.Category == category).Select(x => x.Subcategory).Distinct().ToListAsync();
            
            subcategories.Add(new LinkedSubcategoriesModel()
            {
                Category = category,
                Subcategories = categorySubcategories,
            });
        }
        
        return subcategories;
    }

    public async Task<List<string>> GetSubcategoriesByCategory(string category)
    {
        var subcategories = await _context.Products.Where(x => x.Category == category).Select(x => x.Subcategory).Distinct().ToListAsync();

        return subcategories;
    }
    
    public async Task<List<string>> GetRanges()
    {
        var ranges = await _context.Products.Select(x => x.Range).Distinct().ToListAsync();

        return ranges;
    }
}