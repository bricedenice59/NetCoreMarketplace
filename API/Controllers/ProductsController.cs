using API.Data;
using API.Model;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    
    public ProductsController(ApplicationDbContext context)
    {
        _dbContext = context;
    }
    
    [HttpGet]
    public ActionResult<List<Product>> GetAllProducts()
    {
        var products = _dbContext.Products.ToList();
        return Ok(products);
    }
}
