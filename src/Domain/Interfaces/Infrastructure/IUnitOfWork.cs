namespace ViloesApi.Domain.Interfaces.Infrastructure;

/// <summary>
/// Contém as funções para manipular as transações do banco de dados.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Retorna o repositório para o tipo de entidade provida.
    /// </summary>
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

    /// <summary>
    /// Salva todas as alterações pendentes.
    /// </summary>
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
}
