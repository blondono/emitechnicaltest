using System.Linq.Expressions;
using Emi.Employees.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Emi.Employees.Infrastructure.Persistence;

public class EmployeeRepository<T>(DbContextOptions<EmployeeDbContext> options) : IRepository<T> where T : class
{
    public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        await using var context = new EmployeeDbContext(options);
        context.Entry(entity).State = EntityState.Deleted;
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        await using var db = new EmployeeDbContext(options);
        db.Set<T>().RemoveRange(entities);
        await db.SaveChangesAsync(cancellationToken);
    }

    public async Task<IList<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        await using var context = new EmployeeDbContext(options);
        return await context.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> expression,
    Func<IQueryable<T>, IQueryable<T>> include = null,
    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
    CancellationToken cancellationToken = default)
    {
        await using var context = new EmployeeDbContext(options);
        IQueryable<T> query = context.Set<T>();
        if (include != null)
        {
            query = include(query);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }
        return await query.Where(expression).ToListAsync(cancellationToken);
    }

    public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        await using var context = new EmployeeDbContext(options);
        return await context.Set<T>().FindAsync(id, cancellationToken);
    }

    public async Task InsertAsync(T entity, CancellationToken cancellationToken)
    {
        await using var context = new EmployeeDbContext(options);
        await context.Set<T>().AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        await using var context = new EmployeeDbContext(options);
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);
    }
}
