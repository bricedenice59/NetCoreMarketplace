using System.Linq.Expressions;
using Core.Models;

namespace Core.Specifications;

public class ProductWithTypeAndStockSpecification : BaseSpecification<Product>
{
    public ProductWithTypeAndStockSpecification(Guid id) : base(x => x.Id == id)
    {
        AddInclude(x=>x.ProductType);
        AddInclude(x=>x.ProductStock);
    }
}