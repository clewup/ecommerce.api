using System.ComponentModel.DataAnnotations;

namespace ecommerce.api.Classes;

public class OrderModel
{
    public Guid? Id { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required] 
    public string FirstName { get; set; } = "";
    [Required] 
    public string LastName { get; set; } = "";
    [Required] 
    public string Email { get; set; } = "";
    [Required]
    public AddressModel DeliveryAddress { get; set; }
    [Required]
    public CartModel Cart { get; set; }
    [Required]
    public DateTime OrderDate { get; set; }
}