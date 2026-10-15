namespace ViloesApi.Domain.Common.Validacoes;

public static partial class ValidationExtensions
{
    public static List<ValidacaoFalha> Add(
        this List<ValidacaoFalha> source,
        string propertyName,
        string message)
    {
        source.Add(new ValidacaoFalha(propertyName, message));
        return source;
    }

    public static List<ValidacaoFalha> Join(
        this List<ValidacaoFalha> source,
        params List<ValidacaoFalha>[] validacoesList)
    {
        foreach (var validacoes in validacoesList)
        {
            source.AddRange(validacoes);
        }

        return source;
    }

    public static List<ValidacaoFalha> JoinWhen(
        this List<ValidacaoFalha> source,
        bool shouldJoin,
        Func<List<ValidacaoFalha>, List<ValidacaoFalha>> predicate)
    {
        if (!shouldJoin) { return source; }
        return predicate.Invoke(source);
    }
}
