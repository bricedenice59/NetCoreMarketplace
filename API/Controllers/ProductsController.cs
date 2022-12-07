using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAllProducts()
    {
        var products = await _productRepository.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProducts(Guid id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }
    
    [HttpGet("types")]
    public async Task<ActionResult<ProductType>> GetAllProductTypes()
    {
        var productsTypes = await _productRepository.GetAllProductTypesAsync();
        return Ok(productsTypes);
    }
    
    [HttpGet("stocks/{id}")]
    public async Task<ActionResult<int?>> GetProductStockById(Guid id)
    {
        var product = await _productRepository.GetProductStockByIdAsync(id);
        if (!product.HasValue) return NotFound();
        return Ok(product.Value);
    }
}