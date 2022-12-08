using System.ComponentModel.DataAnnotations;

namespace ecommerce.api.Classes;

public class OrderModel
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public AddressModel DeliveryAddress { get; set; }
    [Required]
    public CartModel Cart { get; set; }
    [Required]
    public DateTime OrderDate { get; set; }
}