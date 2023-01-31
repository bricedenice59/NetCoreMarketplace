using API.Dtos;
using API.Errors;
using API.Helpers;
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
    [API.Attributes.Authorize]
    public async Task<ActionResult<Pagination<ProductDto>>> GetAllProducts([FromQuery]ProductSpecParams productParams)
    {
        var productsSpec = new ProductWithTypeAndStockSpecification(productParams);
        var productsSpecCount = new ProductWithFilterForCountSpecification(productParams);
        var countProducts = await _productRepository.CountAsync(productsSpecCount);
        var products = await _productRepository.ListAsyncWithSpecification(productsSpec);

        var productsDto = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);
        return Ok(new Pagination<ProductDto>(productParams.PageIndex, productParams.PageSize,countProducts, productsDto));
    }

    [HttpGet("{id}")]
    [API.Attributes.Authorize]
    public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
    {
        var productSpec = new ProductWithTypeAndStockSpecification(id);
        var product = await _productRepository.GetEntityWithSpecification(productSpec);
        if (product == null) return NotFound(new ApiResponse(404, $"Product with id: {id.ToString()} does not exist"));

        var productDto = _mapper.Map<Product, ProductDto>(product);
        return Ok(productDto);
    }
    
    [HttpGet("types")]
    [API.Attributes.Authorize]
    public async Task<ActionResult<ProductType>> GetAllProductTypes()
    {
        var productsTypes = await _productTypeRepository.ListAllAsync();
        return Ok(productsTypes);
    }
    
    [HttpGet("stocks/{id}")]
    [API.Attributes.Authorize]
    public async Task<ActionResult<int?>> GetProductStockById(Guid id)
    {
        var pStockSpec = new ProductStockSpecification(id);
        var product = await _productRepository.GetEntityWithSpecification(pStockSpec);
        if (product == null) return NotFound(new ApiResponse(404, $"Cannot fetch product stock, id: {id.ToString()} does not exist"));

        return Ok(product.ProductStock.ProductsLeft);
    }
}