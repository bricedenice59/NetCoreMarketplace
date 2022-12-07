using System.Text.Json;
using Core.Models;
using Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace Infrastructure.SeedData;

public class StoreContextSeed
{
    public static async Task SeedAsync(ApplicationDbContext context, ILoggerFactory loggerFactory)
    {
        if (context == null) return;

        var logger = loggerFactory?.CreateLogger<StoreContextSeed>();

        try
        {
            if (!context.ProductTypes.Any())
            {
                var productTypes = File.OpenRead("../Infrastructure/SeedData/ProductTypes.json");
                var pTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(productTypes);
                foreach (var pType in pTypes) 
                {
                    context.ProductTypes.Add(pType);
                }

                await context.SaveChangesAsync();
            }
            if (!context.Products.Any())
            {
                var productStocks = File.OpenRead("../Infrastructure/SeedData/ProductStocks.json");
                var pStocks = await JsonSerializer.DeserializeAsync<List<ProductStock>>(productStocks);
                foreach (var pStock in pStocks)
                {
                    context.ProductStocks.Add(pStock);
                }

                await context.SaveChangesAsync();
            }
            
            if (!context.Products.Any())
            {
                var productStocks = File.OpenRead("../Infrastructure/SeedData/products.json");
                var products = await JsonSerializer.DeserializeAsync<List<Product>>(productStocks);
                foreach (var product in products)
                {
                    context.Products.Add(product);
                }

                await context.SaveChangesAsync();
            }
        }
        catch (Exception e)
        {
            logger?.LogError(e.Message);
        }
    }
}