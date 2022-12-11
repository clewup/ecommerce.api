using System.ComponentModel.DataAnnotations;

namespace ecommerce.api.Classes;

public class ProductModel
{
    public Guid Id { get; set; }
    public List<ImageModel> Images { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Category { get; set; } = "";
    public string Color { get; set; } = "";
    public double PricePerUnit { get; set; }
    public double Discount { get; set; }
    
}