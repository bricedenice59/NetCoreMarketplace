using Core.Models;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class SpecificationEvaluator<TEntity> where TEntity: BaseModel
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
    {
        var finalQuery = inputQuery;
        if (specification.WhereCriteria != null)
        {
            finalQuery = finalQuery.Where(specification.WhereCriteria);
        }

        if (specification.Includes.Any())
        {
            finalQuery = specification.Includes.Aggregate(finalQuery, (current, include) => current.Include(include));
        }

        return finalQuery;
    }
}