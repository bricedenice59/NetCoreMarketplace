using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductRepository(ApplicationDbContext context)
    {
        _dbContext = context;
    }
    
    public async Task<Product> GetProductByIdAsync(Guid productId)
    {
        return await _dbContext.Products
            .Include(x=>x.ProductStock)
            .Include(x=>x.ProductType)
            .FirstOrDefaultAsync(x => x.Id == productId);
    }

    public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
    {
       return await _dbContext.Products
           .ToListAsync();
    }

    public async Task<IReadOnlyList<ProductType>> GetAllProductTypesAsync()
    {
        return await _dbContext.ProductTypes.ToListAsync();
    }

    public async Task<int?> GetProductStockByIdAsync(Guid productId)
    {
        var stock = await _dbContext.Products
            .Include(x=>x.ProductStock)
            .FirstOrDefaultAsync(x => x.Id == productId);
        return stock?.ProductStock.ProductsLeft;
    }
}