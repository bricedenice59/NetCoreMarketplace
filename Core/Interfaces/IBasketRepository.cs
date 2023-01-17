using Core.Models;

namespace Core.Interfaces;

public interface IBasketRepository
{
    Task<CustomerBasket?> GetCustomerBasket(Guid basketId);

    Task<CustomerBasket?> UpdateCustomerBasket(CustomerBasket basket);

    Task<bool> DeleteCustomerBasket(Guid basketId);
}