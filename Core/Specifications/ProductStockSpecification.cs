using System.Linq.Expressions;
using Core.Models;

namespace Core.Specifications;

public class ProductStockSpecification : BaseSpecification<Product>
{
    public ProductStockSpecification(Guid id) : base(x => x.Id == id)
    {
        AddInclude(x=>x.ProductStock);
    }
}