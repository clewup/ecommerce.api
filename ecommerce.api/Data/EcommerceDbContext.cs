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
    
    public virtual DbSet<CartProductEntity> CartProducts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderEntity>()
            .HasOne(o => o.Cart)
            .WithOne(c => c.Order)
            .HasForeignKey<OrderEntity>(o => o.CartId)
            .IsRequired();

        modelBuilder.Entity<CartEntity>()
            .HasOne(c => c.Order)
            .WithOne(o => o.Cart)
            .HasForeignKey<OrderEntity>(o => o.CartId)
            .IsRequired(false);

        modelBuilder.Entity<CartEntity>()
            .HasMany(c => c.Products)
            .WithMany(p => p.Carts)
            .UsingEntity<CartProductEntity>(
                j => j
                    .HasOne(cp => cp.Product)
                    .WithMany(p => p.CartProducts)
                    .HasForeignKey(cp => cp.ProductId),
                j => j
                    .HasOne(cp => cp.Cart)
                    .WithMany(c => c.CartProducts)
                    .HasForeignKey(cp => cp.CartId),
                j =>
                {
                    j.ToTable("CartProducts");
                    j.Property(cp => cp.DateAdded).HasDefaultValueSql("CURRENT_TIMESTAMP");
                    j.HasKey(cp => new { cp.CartId, cp.ProductId });
                });

        modelBuilder.Entity<ImageEntity>()
            .HasOne(i => i.Product)
            .WithMany(p => p.Images)
            .HasForeignKey(i => i.ProductId)
            .IsRequired();
    }
}