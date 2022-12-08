using Core.Models;
using Core.Specifications;

namespace Core.Interfaces;

public interface IGenericRepository<T> where T : BaseModel
{
    Task<T> GetByIdAsync(Guid id);

    Task<IReadOnlyList<T>> ListAllAsync();

    Task<T> GetEntityWithSpecification(ISpecification<T> specification);

    Task<IReadOnlyList<T>> ListAsyncWithSpecification(ISpecification<T> specification);
}