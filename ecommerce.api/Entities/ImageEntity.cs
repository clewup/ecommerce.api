using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.api.Entities;

public class ImageEntity : BaseEntity
{
    public Uri Url { get; set; }
    
    public Guid ProductId { get; set; }
    public ProductEntity Product { get; set; }
}