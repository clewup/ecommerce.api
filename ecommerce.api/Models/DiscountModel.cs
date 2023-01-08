namespace ecommerce.api.Models;

public class DiscountModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public double Percentage { get; set; }
}