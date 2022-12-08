using System.Linq.Expressions;

namespace Core.Specifications;

public class BaseSpecification<T> : ISpecification<T>
{
    public Expression<Func<T, bool>> WhereCriteria { get; }
    public List<Expression<Func<T, object>>> Includes { get; } = new();

    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        WhereCriteria = criteria;
    }

    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }
}