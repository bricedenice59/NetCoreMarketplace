using Core.Models;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(string productId);
    Task<IReadOnlyList<Product>> GetAllProductsAsync();
}