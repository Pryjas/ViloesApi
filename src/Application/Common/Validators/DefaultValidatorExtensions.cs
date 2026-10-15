using ViloesApi.Domain.Entities;

namespace ViloesApi.Application.Common.Validators;

/// <summary>
/// Métodos de extensão para validators personalizados do sistema.
/// </summary>
public static class DefaultValidatorExtensions
{
    /// <summary>
    /// Adiciona um validator de verificação do valor do identificador ao rule builder.
    /// Validação irá falhar quando o identificador for menor ou igual a zero.
    /// </summary>
    public static IRuleBuilderOptions<T, int> Identificador<T>(this IRuleBuilder<T, int> ruleBuilder)
        => ruleBuilder.SetValidator(new IdentificadorValidator<T>());

    /// <summary>
    /// Adiciona um validator de verificação da existência do identificador.
    /// Validação irá falhar quando o identificador não for encontrado em banco.
    /// </summary>
    /// <typeparam name="T">O tipo do objeto sendo validado.</typeparam>
    /// <typeparam name="TEntity">Entidade dona do identificador a ser consultada em banco.</typeparam>
    /// <param name="ruleBuilder">O builder das regras onde o validator deve ser definido.</param>
    /// <param name="repository">Repository a ser utilizado para consultar a existência do registro.</param>
    public static IRuleBuilderOptions<T, int> MustExists<T, TEntity>(
        this IRuleBuilder<T, int> ruleBuilder,
        IRepository<TEntity> repository) where TEntity : BaseEntity<int>
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(repository);

        return ruleBuilder
            .MustAsync(async (id, cancellation) => await repository.ExistsAsync(e => e.Id == id, cancellation))
            .WithMessage($"Registro de {typeof(TEntity).Name} com id {{PropertyValue}} não encontrado.")
            .WithErrorCode(ValidatorErrorCodes.NaoEncontrado);
    }
}
