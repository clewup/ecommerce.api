using System.ComponentModel.DataAnnotations;

namespace ecommerce.api.Classes;

public class ProductModel
{
    public Guid Id { get; set; }
    public List<string> Images { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Category { get; set; } = "";
    public string Range { get; set; } = "";
    public string Color { get; set; } = "";
    public int Stock { get; set; }
    public double Price { get; set; }
    public double Discount { get; set; }
}