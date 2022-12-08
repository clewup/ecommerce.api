using System.ComponentModel.DataAnnotations;

namespace ecommerce.api.Classes;

public class ProductModel
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public List<ImageModel> Images { get; set; }
    [Required]
    public string Name { get; set; } = "";
    [Required]
    public string Description { get; set; } = "";
    [Required]
    public string Category { get; set; } = "";
    [Required]
    public double PricePerUnit { get; set; }
    [Required]
    public double Discount { get; set; }
    
}