using System.ComponentModel.DataAnnotations;

namespace ecommerce.api.Classes;

public class CartModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public List<ProductModel>? Products { get; set; }
    public DiscountModel? Discount { get; set; }
    public double Total { get; set; }
}