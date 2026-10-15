using ViloesApi.Domain.Interfaces.Infrastructure;

namespace ViloesApi.Infra.Data;

public class UnitOfWork(ViloesDbContext context) : IUnitOfWork
{
    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        => new Repository<TEntity>(context);

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        => await context.SaveChangesAsync(cancellationToken);
}
