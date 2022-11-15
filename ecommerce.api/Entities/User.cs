namespace ecommerce.api.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    
    public string? DeliveryLineOne { get; set; }
    public string? DeliveryLineTwo { get; set; }
    public string? DeliveryLineThree { get; set; }
    public string? DeliveryPostcode { get; set; }
    public string? DeliveryCity { get; set; }
    public string? DeliveryCounty { get; set; }
    public string? DeliveryCountry { get; set; }
    public int? DeliveryBuildingNumber { get; set; }
    public string? DeliveryHouseName { get; set; }
    
    public string? BillingLineOne { get; set; }
    public string? BillingLineTwo { get; set; }
    public string? BillingLineThree { get; set; }
    public string? BillingPostcode { get; set; }
    public string? BillingCity { get; set; }
    public string? BillingCounty { get; set; }
    public string? BillingCountry { get; set; }
    public int? BillingBuildingNumber { get; set; }
    public string? BillingHouseName { get; set; }
}