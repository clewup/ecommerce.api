using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using ecommerce.api.Classes;

namespace ecommerce.api.Entities;

public class OrderEntity : BaseEntity
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public CartEntity Cart { get; set; }
    [Required]
    public DateTime OrderDate { get; set; }
}