namespace ecommerce.api.Classes;

public class Order
{
    public Guid Id { get; set; }
    public User? User { get; set; }
    public Cart? Cart { get; set; }
}