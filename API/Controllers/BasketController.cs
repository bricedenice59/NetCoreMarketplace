using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BasketController : BaseApiController
{
    private readonly IBasketRepository _basketRepository;

    public BasketController(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }
    
    [HttpGet]
    public async Task<ActionResult<CustomerBasket>> GetBasketById(Guid id)
    {
        var customerBasket = await _basketRepository.GetCustomerBasket(id);
        return Ok(customerBasket ?? new CustomerBasket(id));
    }
    
    [HttpPost]
    public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
    {
        var customerBasket = await _basketRepository.UpdateCustomerBasket(basket);
        return Ok(customerBasket);
    }
    
    [HttpDelete]
    public async Task DeleteBasket(Guid id)
    {
        await _basketRepository.DeleteCustomerBasket(id);
    }
}