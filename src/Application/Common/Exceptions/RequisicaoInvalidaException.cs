using ViloesApi.Domain.Common.Validacoes;

namespace ViloesApi.Application.Common.Exceptions;

/// <summary>
/// Lançada quando a requisição não traz os campos esperados (falha dos validators do FluentValidation).
/// </summary>
public class RequisicaoInvalidaException(IEnumerable<ValidacaoFalha> falhas)
    : Exception("Um ou mais campos da requisição são inválidos.")
{
    public List<ValidacaoFalha> Falhas { get; } = falhas.ToList();
}
