using System.Linq.Expressions;
using Core.Models;

namespace Core.Specifications;

public class ProductWithTypeAndStockSpecification : BaseSpecification<Product>
{
    public ProductWithTypeAndStockSpecification(string? sortExpression) : base()
    {
        AddInclude(x=>x.ProductType);
        AddInclude(x=>x.ProductStock);

        if (string.IsNullOrEmpty(sortExpression)) return;
        {
            switch (sortExpression)
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