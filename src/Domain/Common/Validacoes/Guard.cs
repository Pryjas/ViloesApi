using ViloesApi.Domain.Exceptions;

namespace ViloesApi.Domain.Common.Validacoes;

public static class Guard
{
    public static void Enforce(List<ValidacaoFalha> falhas)
    {
        if (falhas is null) { return; }
        if (falhas.Count is 0) { return; }
        throw new ValidacaoException(falhas);
    }

    public static void NotNull(object value, string propertyName)
    {
        Enforce(Validacao.Init.NotNull(value, propertyName));
    }
}
