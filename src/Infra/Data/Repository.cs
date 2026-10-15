using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ViloesApi.Domain.Common;
using ViloesApi.Domain.Interfaces.Infrastructure;

namespace ViloesApi.Infra.Data;

public class Repository<T>(ViloesDbContext context) : IRepository<T> where T : class
{
    private readonly DbSet<T> dbSet = context.Set<T>();

    public void Add<TEntity>(TEntity entity) where TEntity : T, IAggregateRoot
        => dbSet.Add(entity);

    public void Delete<TEntity>(TEntity entity) where TEntity : T, IAggregateRoot
        => dbSet.Remove(entity);

    public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        => dbSet.Where(predicate);

    public IQueryable<T> GetAll()
        => dbSet;

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        => await dbSet.AnyAsync(predicate, cancellationToken);
}
