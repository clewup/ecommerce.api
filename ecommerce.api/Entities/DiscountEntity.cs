using System.ComponentModel.DataAnnotations;

namespace ecommerce.api.Entities;

public class DiscountEntity : BaseEntity
{
    public string Code { get; set; } = "";
    public double PercentOff { get; set; }
    public string Description { get; set; } = "";
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
}