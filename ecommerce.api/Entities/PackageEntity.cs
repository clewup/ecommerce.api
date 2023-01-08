using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.api.Entities;

public class PackageEntity : BaseEntity
{
    public DateTime ShippedDate { get; set; }
    public DateTime ArrivalDate { get; set; }
    
    public Guid OrderId { get; set; }
    public OrderEntity Order { get; set; }
}