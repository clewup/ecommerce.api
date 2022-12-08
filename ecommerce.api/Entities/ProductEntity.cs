using System.ComponentModel.DataAnnotations;
using ecommerce.api.Classes;

namespace ecommerce.api.Entities;

public class ProductEntity : BaseEntity
{
    [Required]
    public string Name { get; set; } = "";
    public List<ImageEntity> Images { get; set; }
    [Required]
    public string Description { get; set; } = "";
    [Required]
    public string Category { get; set; } = "";
    [Required]
    public double PricePerUnit { get; set; }
    [Required]
    public double Discount { get; set; }
}