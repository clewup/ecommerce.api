using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.api.Entities;

public class CartEntity : BaseEntity
{
    public Guid UserId { get; set; }
    public double Total { get; set; }
    
    public ICollection<CartProductEntity> Products { get; set; }
    
    public Guid OrderId { get; set; }
    public OrderEntity Order { get; set; }
}