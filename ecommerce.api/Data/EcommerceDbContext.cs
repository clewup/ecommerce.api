using ecommerce.api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.api.Data;

public class EcommerceDbContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public EcommerceDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(Configuration.GetConnectionString("EcommerceConnection"));
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
    
    public virtual DbSet<CartEntity> Carts { get; set; }
    public virtual DbSet<CartItemEntity> CartItems { get; set; }
    public virtual DbSet<OrderEntity> Orders { get; set; }
    public virtual DbSet<ProductEntity> Products { get; set; }
}