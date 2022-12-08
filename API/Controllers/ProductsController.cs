using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IGenericRepository<Product> _productRepository;
    private readonly IGenericRepository<ProductType> _productTypeRepository;

    public ProductsController(IGenericRepository<Product> productRepository, IGenericRepository<ProductType> productTypeRepository)
    {
        _productRepository = productRepository;
        _productTypeRepository = productTypeRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAllProducts()
    {
        var products = await _productRepository.ListAllAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProducts(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }
    
    [HttpGet("types")]
    public async Task<ActionResult<ProductType>> GetAllProductTypes()
    {
        var productsTypes = await _productTypeRepository.ListAllAsync();
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