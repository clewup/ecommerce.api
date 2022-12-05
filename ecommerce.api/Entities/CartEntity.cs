using System.ComponentModel.DataAnnotations;
using ecommerce.api.Classes;

namespace ecommerce.api.Entities;

public class CartEntity : BaseEntity
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public List<ProductEntity> Products { get; set; }
    [Required]
    public double Total { get; set; }
}