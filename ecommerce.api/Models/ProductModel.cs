using System.ComponentModel.DataAnnotations;

namespace ecommerce.api.Models;

public class ProductModel
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; } = "";
    [Required]
    public string Description { get; set; } = "";
    [Required]
    public string Color { get; set; } = "";
    [Required]
    public List<string> Images { get; set; }
    [Required]
    public string Category { get; set; } = "";
    [Required]
    public string Subcategory { get; set; } = "";
    [Required]
    public string Range { get; set; } = "";
    [Required]
    public string Size { get; set; }
    [Required]
    [Range(0, 2000)]
    public int Stock { get; set; }
    [Required]
    [Range(5, 2000)]
    public double Price { get; set; }
    public DiscountModel? Discount { get; set; }
}