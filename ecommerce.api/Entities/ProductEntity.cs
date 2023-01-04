using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.api.Entities;

public class ProductEntity : BaseEntity
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Color { get; set; } = "";
    public List<ImageEntity> Images { get; set; }
    
    public string Category { get; set; } = "";
    public string Subcategory { get; set; } = "";
    public string Range { get; set; } = "";
    
    public bool OneSize { get; set; }
    public int XSmall { get; set; }
    public int Small { get; set; }
    public int Medium { get; set; }
    public int Large { get; set; }
    public int XLarge { get; set; }
    
    public double Price { get; set; }
    public double Discount { get; set; }
    public double? DiscountedPrice { get; set; }
    public double? TotalSavings { get; set; }
    
    public List<CartEntity> Carts { get; set; }
    public List<CartProductEntity> CartProducts { get; set; }
    public List<OrderEntity> Orders { get; set; }
    public List<OrderProductEntity> OrderProducts { get; set; }
}