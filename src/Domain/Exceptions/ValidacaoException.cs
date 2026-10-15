using ViloesApi.Domain.Common.Validacoes;

namespace ViloesApi.Domain.Exceptions;

/// <summary>
/// Lançada quando uma regra de negócio (invariante do domínio) é violada.
/// </summary>
public class ValidacaoException(IEnumerable<ValidacaoFalha> falhas)
    : Exception("Uma ou mais regras de negócio foram violadas.")
{
    public List<ValidacaoFalha> Falhas { get; } = falhas.ToList();
}
