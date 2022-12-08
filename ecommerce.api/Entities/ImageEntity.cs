using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.api.Entities;

public class ImageEntity : BaseEntity
{
    [Required]
    public Guid ProductId { get; set; }
    [Required]
    public Uri Url { get; set; }
}