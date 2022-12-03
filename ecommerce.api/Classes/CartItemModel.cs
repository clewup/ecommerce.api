namespace ecommerce.api.Classes;

public class CartItemModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }  = "";
    public string Variant { get; set; }  = "";
    public int Quantity { get; set; }
    public double PricePerUnit { get; set; }
    public bool isDiscounted { get; set; }
    public double Discount { get; set; }
}