using System.Reflection;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }
 
    // Add the DbSets for each of our models we would like at our database
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<ProductStock> ProductStocks { get; set; }
    
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductImage>()
            .HasOne<Product>()
            .WithMany(p=>p.ProductImages)
            .HasForeignKey(p => p.ProductId);
    }
}