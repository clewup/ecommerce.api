namespace ecommerce.api.Classes;

public class CartModel
{
    public List<CartItemModel>? CartItems { get; set; }
    public double Total { get; set; }
}