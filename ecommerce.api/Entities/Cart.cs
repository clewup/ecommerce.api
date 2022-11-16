using ecommerce.api.Classes;

namespace ecommerce.api.Entities;

public class Cart
{
    public Guid Id { get; set; }
    public List<SelectedProduct>? SelectedProduct { get; set; }
    public double Total { get; set; }
}