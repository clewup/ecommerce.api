using System.ComponentModel.DataAnnotations;

namespace ecommerce.api.Models;

public class OrderModel
{
    public Guid Id { get; set; }
    
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
    public List<ProductModel> Products { get; set; }
    
    public double Total { get; set; }
    
    public double? DiscountedTotal { get; set; }
    
    public double? TotalSavings { get; set; }
    
    public DateTime OrderDate { get; set; }
}