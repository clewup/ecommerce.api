using System.ComponentModel.DataAnnotations;
using ecommerce.api.Classes;

namespace ecommerce.api.Entities;

public class CartEntity : BaseEntity
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public List<Guid> CartItemIds { get; set; }
    [Required]
    public double Total { get; set; }
    
    public string? DiscountCode { get; set; }
    public double? DiscountedTotal { get; set; }
}