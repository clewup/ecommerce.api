namespace ecommerce.api.Classes;

public class OrderModel
{
    public Guid Id { get; set; }
    public UserModel? User { get; set; }
    public CartModel? Cart { get; set; }
    public DateTime? OrderDate { get; set; }
    public bool IsShipped { get; set; } = false;
    public DateTime? ShippedDate { get; set; }
}