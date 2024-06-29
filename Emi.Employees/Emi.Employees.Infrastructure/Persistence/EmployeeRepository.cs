using System.Linq.Expressions;
using Emi.Employees.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Emi.Employees.Infrastructure.Persistence
{
    public class EmployeeRepository<T>(DbContextOptions<EmployeeDbContext> options) : IRepository<T> where T : class
    {
        public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            await using var context = new EmployeeDbContext(options);
            context.Entry(entity).State = EntityState.Deleted;
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
        {
            await using var context = new EmployeeDbContext(options);
            return await context.Set<T>().Where(expression).ToListAsync(cancellationToken);
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
}
