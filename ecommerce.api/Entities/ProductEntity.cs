using ecommerce.api.Classes;

namespace ecommerce.api.Entities;

public class ProductEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public string Category { get; set; } = "";
    public List<StockModel>? Stock { get; set; }
    public double PricePerUnit { get; set; }
    public bool IsDiscounted { get; set; }
    public double? Discount { get; set; }
}