using System.ComponentModel.DataAnnotations;

namespace ecommerce.api.Classes;

public class DiscountModel
{
    public string Code { get; set; } = "";
    public double PercentOff { get; set; }
    public string Description { get; set; } = "";
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
}