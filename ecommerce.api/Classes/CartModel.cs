using System.ComponentModel.DataAnnotations;

namespace ecommerce.api.Classes;

public class CartModel
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public List<ProductModel> Products { get; set; }
    [Required]
    public double Total { get; set; }
}