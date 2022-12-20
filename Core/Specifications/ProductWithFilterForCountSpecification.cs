using System.Linq.Expressions;
using Core.Models;

namespace Core.Specifications;

public class ProductWithFilterForCountSpecification : BaseSpecification<Product>
{
    public ProductWithFilterForCountSpecification(ProductSpecParams productParams) 
        : base((x) => !productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId.Value)
    {

    }
}