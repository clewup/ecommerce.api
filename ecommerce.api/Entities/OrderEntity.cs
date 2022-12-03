using System.Diagnostics.CodeAnalysis;
using ecommerce.api.Classes;

namespace ecommerce.api.Entities;

public class OrderEntity
{
    public Guid Id { get; set; }
    public UserModel User { get; set; }
    public CartModel Cart { get; set; }
    public DateTime OrderDate { get; set; }
    public bool IsShipped { get; set; }
    public DateTime? ShippedDate { get; set; }
}