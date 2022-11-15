using ecommerce.api.Classes;

namespace ecommerce.api.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public List<Stock>? Stock { get; set; }
    public double PricePerUnit { get; set; }
    public bool IsDiscounted { get; set; }
    public double? Discount { get; set; }
}