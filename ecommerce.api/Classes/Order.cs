namespace ecommerce.api.Classes;

public class Order
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Cart? Cart { get; set; }
}