namespace ViloesApi.Domain.Common.Validacoes;

public static partial class ValidationExtensions
{
    public static List<ValidacaoFalha> Null(
        this List<ValidacaoFalha> source,
        object value,
        string propertyName)
        => Null(source, value, propertyName, $"O campo '{propertyName}' deve ser nulo.");

    public static List<ValidacaoFalha> Null(
        this List<ValidacaoFalha> source,
        object value,
        string propertyName,
        string message)
    {
        if (value is null) { return source; }

        return source.Add(propertyName, message);
    }

    public static List<ValidacaoFalha> NotNull(
        this List<ValidacaoFalha> source,
        object value,
        string propertyName)
        => NotNull(source, value, propertyName, $"O campo '{propertyName}' não foi informado.");

    public static List<ValidacaoFalha> NotNull(
        this List<ValidacaoFalha> source,
        object value,
        string propertyName,
        string message)
    {
        if (value is not null) { return source; }

        return source.Add(propertyName, message);
    }
}
