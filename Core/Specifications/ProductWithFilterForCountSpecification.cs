using System.Linq.Expressions;
using Core.Models;

namespace Core.Specifications;

public class ProductWithFilterForCountSpecification : BaseSpecification<Product>
{
    public ProductWithFilterForCountSpecification(ProductSpecParams productParams) 
        : base((x) =>  
            (string.IsNullOrEmpty(productParams.SearchCriteria) || x.Name.ToLower().Contains(productParams.SearchCriteria))
            && (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId.Value))
    {

    }
}