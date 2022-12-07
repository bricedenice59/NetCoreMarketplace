using Core.Models;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(Guid productId);
    
    Task<IReadOnlyList<Product>> GetAllProductsAsync();
    
    Task<IReadOnlyList<ProductType>> GetAllProductTypesAsync();
    
    Task<int?> GetProductStockByIdAsync(Guid productId);
}