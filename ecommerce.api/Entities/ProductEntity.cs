using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ecommerce.api.Classes;

namespace ecommerce.api.Entities;

public class ProductEntity : BaseEntity
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Category { get; set; } = "";
    public string Range { get; set; } = "";
    public string Color { get; set; } = "";
    public int Stock { get; set; }
    public double Price { get; set; }
    public double Discount { get; set; }
    
    public ICollection<ImageEntity> Images { get; set; }
    public ICollection<CartEntity> Carts { get; set; }
    public List<CartProductEntity> CartProducts { get; set; }

}