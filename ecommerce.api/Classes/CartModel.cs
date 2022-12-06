namespace ecommerce.api.Classes;

public class CartModel
{
    public Guid UserId { get; set; }
    public List<ProductModel> Products { get; set; }
    public double Total { get; set; }
}