namespace ecommerce.api.Classes;

public class CartModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public List<ProductModel> Products { get; set; } = new List<ProductModel>();
    public double Total { get; set; }
}