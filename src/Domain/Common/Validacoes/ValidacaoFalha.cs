namespace ViloesApi.Domain.Common.Validacoes;

/// <summary>
/// Define uma falha de validação.
/// </summary>
public record ValidacaoFalha(string PropertyName, string ErrorMessage)
{
    public override string ToString() => PropertyName + " : " + ErrorMessage;
}
