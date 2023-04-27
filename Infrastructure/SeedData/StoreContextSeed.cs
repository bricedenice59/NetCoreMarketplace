using System.Diagnostics;
using System.Reflection;
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
                string pathToProductTypesJson = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"SeedData/ProductTypes.json");
                Trace.WriteLine(pathToProductTypesJson);
                var productTypes = File.OpenRead(pathToProductTypesJson);
                var pTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(productTypes);
                foreach (var pType in pTypes) 
                {
                    context.ProductTypes.Add(pType);
                }

                await context.SaveChangesAsync();
            }
            if (!context.ProductStocks.Any())
            {
                string pathToProductStocksJson = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"SeedData/ProductStocks.json");
                Trace.WriteLine(pathToProductStocksJson);
                var productStocks = File.OpenRead(pathToProductStocksJson);
                var pStocks = await JsonSerializer.DeserializeAsync<List<ProductStock>>(productStocks);
                foreach (var pStock in pStocks)
                {
                    context.ProductStocks.Add(pStock);
                }

                await context.SaveChangesAsync();
            }
            if (!context.Products.Any())
            {
                string pathToProductContentJsom = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"SeedData/products.json");
                var productContent = File.OpenRead(pathToProductContentJsom);
                var products = await JsonSerializer.DeserializeAsync<List<Product>>(productContent);
                foreach (var product in products)
                {
                    context.Products.Add(product);
                }

                await context.SaveChangesAsync();
            }
            
            if (!context.ProductImages.Any())
            {
                string pathToProductImagesJsom = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"SeedData/ProductImages.json");
                var productImages = File.OpenRead(pathToProductImagesJsom);
                var pImages = await JsonSerializer.DeserializeAsync<List<ProductImage>>(productImages);
                foreach (var pImage in pImages)
                {
                    context.ProductImages.Add(pImage);
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