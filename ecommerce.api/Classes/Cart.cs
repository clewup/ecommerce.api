namespace ecommerce.api.Classes;

public class Cart
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public List<CartItem> CartItems { get; set; }
    public double Total { get; set; }
}