using System.ComponentModel.DataAnnotations;

namespace ecommerce.api.Models;

public class AddressModel
{
    [Required]
    public string LineOne { get; set; } = "";
    
    [Required]
    public string LineTwo { get; set; } = "";
    
    public string LineThree { get; set; } = "";
    
    [Required]
    public string Postcode { get; set; } = "";
    
    [Required]
    public string City { get; set; } = "";
    
    public string County { get; set; } = "";
    
    [Required]
    public string Country { get; set; } = "";
}