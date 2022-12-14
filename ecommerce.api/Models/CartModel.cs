using System.ComponentModel.DataAnnotations;

namespace ecommerce.api.Models;

public class CartModel
{
    public Guid Id { get; set; }
    [Required]
    public Guid UserId { get; set; }
    public double Total { get; set; }
    
    public List<ProductModel> Products { get; set; }
}