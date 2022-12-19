using System.Linq.Expressions;
using Core.Models;

namespace Core.Specifications;

public class ProductWithTypeAndStockSpecification : BaseSpecification<Product>
{
    public ProductWithTypeAndStockSpecification(ProductSpecParams productParams) 
        : base((x) => !productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId.Value)
    {
        AddInclude(x=>x.ProductType);
        AddInclude(x=>x.ProductStock);
        ApplyPaging(productParams.PageSize * (productParams.PageIndex -1), productParams.PageSize);
        
        if (string.IsNullOrEmpty(productParams.SortBy)) return;
        {
            switch (productParams.SortBy)
            {
                case "priceAsc":
                    AddOrderBy(x=>x.PriceWithExcludedVAT);
                    break;
                case "priceDesc":
                    AddOrderByDescending( x=>x.PriceWithExcludedVAT);
                    break;
                default: AddOrderBy(x=>x.Name);
                    break;
            }
        }
    }
    
    public ProductWithTypeAndStockSpecification(Guid id) : base(x => x.Id == id)
    {
        AddInclude(x=>x.ProductType);
        AddInclude(x=>x.ProductStock);
    }
}