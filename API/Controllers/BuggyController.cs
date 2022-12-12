using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BuggyController : BaseApiController
{
    private readonly ApplicationDbContext _dbContext;
    
    public BuggyController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet("notFound")]
    public async Task<ActionResult> NotFoundRequest()
    {
        var notfoundProduct = await _dbContext.Products.FindAsync(Guid.NewGuid());
        if (notfoundProduct == null)
            return NotFound(new ApiResult(404));
        
        return Ok();
    }
    
    [HttpGet("serverError")]
    public async Task<ActionResult> GetServerError()
    {
        var notfoundProduct = await _dbContext.Products.FindAsync(Guid.NewGuid());
        var notfoundProductStr = notfoundProduct.ToString();
        
        return Ok();
    }
    
    [HttpGet("badRequest")]
    public ActionResult GetBadRequest()
    {
        return BadRequest(new ApiResult(400));
    }
    
    [HttpGet("badRequest/{id}")]
    public async Task<ActionResult> GetBadRequest(Guid id)
    {
        return Ok();
    }
}