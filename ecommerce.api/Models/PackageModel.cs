namespace ecommerce.api.Models;

public class PackageModel
{
    public Guid TrackingNumber { get; set; }
    public DateTime ShippedDate { get; set; }
    public DateTime ArrivalDate { get; set; }
}