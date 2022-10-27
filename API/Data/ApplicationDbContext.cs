using API.Model;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }
 
    // Add the DbSets for each of our models we would like at our database
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<ProductStock> ProductStocks { get; set; }
    public DbSet<Product> Products { get; set; }
}