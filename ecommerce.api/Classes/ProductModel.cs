namespace ecommerce.api.Classes;

public class ProductModel
{
    public Guid Id { get; set; }
    public List<string> Images { get; set; } = new List<string>();
    public string Name { get; set; } = "";
    public string Description { get; set; }
    public string Category { get; set; } = "";
    public List<StockModel> Stock { get; set; } = new List<StockModel>();
    public double PricePerUnit { get; set; }
    public bool IsDiscounted { get; set; }
    public double Discount { get; set; }
    
}