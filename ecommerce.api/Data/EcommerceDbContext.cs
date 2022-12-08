using ecommerce.api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.api.Data;

public class EcommerceDbContext : DbContext
{
    public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options):base(options) {  }
    
    public virtual DbSet<CartEntity> Carts { get; set; }
    public virtual DbSet<OrderEntity> Orders { get; set; }
    public virtual DbSet<ProductEntity> Products { get; set; }
    public virtual DbSet<ImageEntity> Images { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CartProductEntity>().HasKey(i => new { i.CartId, i.ProductId });

        modelBuilder.Entity<OrderEntity>()
            .HasOne(o => o.Cart)
            .WithOne(c => c.Order)
            .HasForeignKey<CartEntity>(c => c.OrderId);
    }
}