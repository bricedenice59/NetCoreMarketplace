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
    
    public async Task<Product> GetProductByIdAsync(string productId)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(x => x.Id.ToString() == productId);

    }

    public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
    {
       return await _dbContext.Products.ToListAsync();
    }
}