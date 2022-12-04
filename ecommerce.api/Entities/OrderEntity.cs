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
    public Guid CartId { get; set; }
    [Required]
    public DateTime OrderDate { get; set; }
    public DateTime? ShippedDate { get; set; }
}