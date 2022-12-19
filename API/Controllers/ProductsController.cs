using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductsController : BaseApiController
{
    private readonly IGenericRepository<Product> _productRepository;
    private readonly IGenericRepository<ProductType> _productTypeRepository;
    private readonly IMapper _mapper;

    public ProductsController(IGenericRepository<Product> productRepository,
        IGenericRepository<ProductType> productTypeRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _productTypeRepository = productTypeRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAllProducts(string? sortBy, Guid? typeId)
    {
        var productsSpec = new ProductWithTypeAndStockSpecification(sortBy, typeId);
        var products = await _productRepository.ListAsyncWithSpecification(productsSpec);
        
        var productsDto = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);
        return Ok(productsDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
    {
        var productSpec = new ProductWithTypeAndStockSpecification(id);
        var product = await _productRepository.GetEntityWithSpecification(productSpec);
        if (product == null) return NotFound(new ApiResponse(404, $"Product with id: {id.ToString()} does not exist"));

        var productDto = _mapper.Map<Product, ProductDto>(product);
        return Ok(productDto);
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
        var pStockSpec = new ProductStockSpecification(id);
        var product = await _productRepository.GetEntityWithSpecification(pStockSpec);
        if (product == null) return NotFound(new ApiResponse(404, $"Cannot fetch product stock, id: {id.ToString()} does not exist"));

        return Ok(product.ProductStock.ProductsLeft);
    }
}