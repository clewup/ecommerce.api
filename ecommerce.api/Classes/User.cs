namespace ecommerce.api.Classes;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public Address? DeliveryAddress { get; set; }
    public Address? BillingAddress { get; set; }
}