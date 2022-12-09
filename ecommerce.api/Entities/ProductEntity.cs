using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ecommerce.api.Classes;

namespace ecommerce.api.Entities;

public class ProductEntity : BaseEntity
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Category { get; set; } = "";
    public double PricePerUnit { get; set; }
    public double Discount { get; set; }
    
    public ICollection<ImageEntity> Images { get; set; }
    
    public ICollection<CartEntity> Carts { get; set; }
    public List<CartProductEntity> CartProducts { get; set; }

}