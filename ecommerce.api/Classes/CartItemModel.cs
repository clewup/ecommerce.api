namespace ecommerce.api.Classes;

public class CartItemModel
{
    public Guid Id { get; set; }
    public List<string> Images { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Category { get; set; } = "";
    public int Quantity { get; set; }
    public double PricePerUnit { get; set; }
    public double Discount { get; set; }
}