namespace ecommerce.api.Classes;

public class OrderModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public AddressModel DeliveryAddress { get; set; } = new AddressModel();
    public CartModel Cart { get; set; } = new CartModel();
    public DateTime OrderDate { get; set; }
}