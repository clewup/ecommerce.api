using ecommerce.api.Classes;

namespace ecommerce.api.Entities;

public class CartEntity
{
    public List<CartItemModel>? CartItems { get; set; }
    public Guid UserId { get; set; }
    public double Total { get; set; }
}