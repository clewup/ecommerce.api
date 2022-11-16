namespace ecommerce.api.Classes;

public class Cart
{
    public Guid Id { get; set; }
    public List<SelectedProduct>? SelectedProduct { get; set; }
    public double Total { get; set; }
}