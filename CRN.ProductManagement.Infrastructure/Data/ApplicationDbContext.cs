using CRN.ProductManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRN.ProductManagement.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();

    public DbSet<Item> Items => Set<Item>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.ProductName)
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(x => x.CreatedBy)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.CreatedOn)
                .IsRequired();

            entity.Property(x => x.ModifiedBy)
                .HasMaxLength(100);
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.ToTable("Item");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Quantity)
                .IsRequired();

            entity.HasOne(x => x.Product)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}