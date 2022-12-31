using System.ComponentModel.DataAnnotations;

namespace ecommerce.api.Models;

public class ProductModel
{
    public Guid Id { get; set; }
    
    [Required]
    public List<string> Images { get; set; }
    
    [Required]
    public string Name { get; set; } = "";
    
    [Required]
    public string Description { get; set; } = "";
    
    [Required]
    public string Category { get; set; } = "";
    
    public string? Subcategory { get; set; }
    
    [Required]
    public string Range { get; set; } = "";
    
    [Required]
    public string Color { get; set; } = "";
    
    [Required]
    [Range(1, 200)]
    public int Stock { get; set; }
    
    [Required]
    [Range(5, 2000)]
    public double Price { get; set; }
    
    [Required]
    [Range(0, 75)]
    public double Discount { get; set; }
    
    public double? DiscountedPrice { get; set; }
    
    public double? TotalSavings { get; set; }
}