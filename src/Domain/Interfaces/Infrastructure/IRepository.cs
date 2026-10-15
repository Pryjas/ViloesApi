using System.Linq.Expressions;
using ViloesApi.Domain.Common;

namespace ViloesApi.Domain.Interfaces.Infrastructure;

/// <summary>
/// Repositório genérico. Inclusão e remoção só são permitidas para raízes de agregado.
/// </summary>
public interface IRepository<T> where T : class
{
    void Add<TEntity>(TEntity entity) where TEntity : T, IAggregateRoot;

    void Delete<TEntity>(TEntity entity) where TEntity : T, IAggregateRoot;

    IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

    IQueryable<T> GetAll();

    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
}
