using ecommerce.api.Infrastructure;

namespace ecommerce.api.Models;

public class SearchCriteriaModel
{
    public string SearchTerm { get; set; } = "";
    public string Category { get; set; } = "";
    public string Subcategory { get; set; } = "";
    public string Range { get; set; } = "";
    public string InStock { get; set; } = "";
    public string OnSale { get; set; } = "";
    public string MinPrice { get; set; } = "";
    public string MaxPrice { get; set; } = "";
    public string SortBy { get; set; } = "";
    public string SortVariation { get; set; } = "";
}