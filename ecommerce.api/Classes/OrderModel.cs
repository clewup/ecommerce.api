namespace ecommerce.api.Classes;

public class OrderModel
{
    public Guid Id { get; set; }
    public UserModel User { get; set; }
    public CartModel Cart { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime? ShippedDate { get; set; }
}