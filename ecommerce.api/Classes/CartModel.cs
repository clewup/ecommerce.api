namespace ecommerce.api.Classes;

public class CartModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public List<CartItemModel> CartItems { get; set; }
    public double Total { get; set; }
    public DiscountModel? DiscountCode { get; set; }
    public double? DiscountedTotal { get; set; }
}