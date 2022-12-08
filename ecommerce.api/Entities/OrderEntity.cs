using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using ecommerce.api.Classes;

namespace ecommerce.api.Entities;

public class OrderEntity : BaseEntity
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public string LineOne { get; set; } = "";
    [Required]
    public string LineTwo { get; set; } = "";
    [Required]
    public string LineThree { get; set; } = "";
    [Required]
    public string Postcode { get; set; } = "";
    [Required]
    public string City { get; set; } = "";
    [Required]
    public string County { get; set; } = "";
    [Required]
    public string Country { get; set; } = "";
    [Required]
    public CartEntity Cart { get; set; }
    [Required]
    public DateTime OrderDate { get; set; }
}