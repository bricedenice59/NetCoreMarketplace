using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BasketController : BaseApiController
{
    private readonly IBasketRepository _basketRepository;
    private readonly IMapper _mapper;

    public BasketController(IBasketRepository basketRepository, IMapper mapper)
    {
        _basketRepository = basketRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<ActionResult<CustomerBasket>> GetBasketById(Guid id)
    {
        var customerBasket = await _basketRepository.GetCustomerBasket(id);
        return Ok(customerBasket ?? new CustomerBasket(id));
    }
    
    [HttpPost]
    public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basketDto)
    {
        var customerBasket = _mapper.Map<CustomerBasket>(basketDto);
        var updatedBasket = await _basketRepository.UpdateCustomerBasket(customerBasket);
        return Ok(updatedBasket);
    }
    
    [HttpDelete]
    public async Task DeleteBasket(Guid id)
    {
        await _basketRepository.DeleteCustomerBasket(id);
    }
}