using ecommerce.api.Classes;

namespace ecommerce.api.Entities;

public class CartEntity
{
    public Guid UserId { get; set; }
    public List<CartItemModel> CartItems { get; set; }
    public double Total { get; set; }
    public DiscountCodeModel? DiscountCode { get; set; }
    public double? DiscountedTotal { get; set; }
}