using ecommerce.api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.api.Data;

public class EcommerceDbContext : DbContext
{
    public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options):base(options) {  }
    
    public virtual DbSet<OrderEntity> Orders { get; set; }
    public virtual DbSet<CartEntity> Carts { get; set; }
    public virtual DbSet<ProductEntity> Products { get; set; }
    public virtual DbSet<ImageEntity> Images { get; set; }
    public virtual DbSet<PackageEntity> Packages { get; set; }
    public virtual DbSet<ConfigEntity> Config { get; set; }
    public virtual DbSet<DiscountEntity> Discounts { get; set; }
    public virtual DbSet<PromotionEntity> Promotions { get; set; }
    
    public virtual DbSet<OrderProductEntity> OrderProducts { get; set; }
    public virtual DbSet<CartProductEntity> CartProducts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderEntity>()
            .HasMany(o => o.Products)
            .WithMany(p => p.Orders)
            .UsingEntity<OrderProductEntity>(
                j => j
                    .HasOne(op => op.Product)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(cp => cp.ProductId),
                j => j
                    .HasOne(op => op.Order)
                    .WithMany(o => o.OrderProducts)
                    .HasForeignKey(op => op.OrderId),
                j =>
                {
                    j.ToTable("OrderProducts");
                    j.HasKey(op => new { op.OrderId, op.ProductId });
                });

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
                    j.HasKey(cp => new { cp.CartId, cp.ProductId });
                });

        modelBuilder.Entity<ImageEntity>()
            .HasOne(i => i.Product)
            .WithMany(p => p.Images)
            .HasForeignKey(i => i.ProductId)
            .IsRequired();

        modelBuilder.Entity<PackageEntity>()
            .HasOne(p => p.Order)
            .WithOne(o => o.Package)
            .HasForeignKey<OrderEntity>(o => o.TrackingNumber)
            .IsRequired(false);

        modelBuilder.Entity<PromotionEntity>()
            .HasOne(p => p.Discount)
            .WithMany(d => d.Promotions)
            .HasForeignKey(p => p.DiscountId)
            .IsRequired();

        modelBuilder.Entity<DiscountEntity>()
            .HasMany(d => d.Products)
            .WithOne(p => p.Discount)
            .HasForeignKey(p => p.DiscountId)
            .IsRequired(false);
    }
}