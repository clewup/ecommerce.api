using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.api.Entities;

public class CartEntity : BaseEntity
{
    public Guid UserId { get; set; }
    public double Total { get; set; }
    
    public List<ProductEntity> Products { get; set; }
    public List<CartProductEntity> CartProducts { get; set; }
}