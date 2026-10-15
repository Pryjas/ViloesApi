namespace ViloesApi.Domain.Common.Validacoes;

public static partial class ValidationExtensions
{
    public static List<ValidacaoFalha> NotContainsAny<TList>(
        this List<ValidacaoFalha> source,
        IEnumerable<TList> value,
        Func<TList, bool> predicate,
        string propertyName,
        string message)
    {
        if (!value.Any(predicate)) { return source; }

        return source.Add(propertyName, message);
    }
}
