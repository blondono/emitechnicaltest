using System.Linq.Expressions;

namespace Emi.Employees.Domain.Shared;

public interface IRepository<T> where T : class
{
    Task<IList<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> expression,
    Func<IQueryable<T>, IQueryable<T>> include = null,
    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
    CancellationToken cancellationToken = default);
    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task InsertAsync(T entity, CancellationToken cancellationToken);
    Task UpdateAsync(T entity, CancellationToken cancellationToken);
    Task DeleteAsync(T entity, CancellationToken cancellationToken);
    Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
}
