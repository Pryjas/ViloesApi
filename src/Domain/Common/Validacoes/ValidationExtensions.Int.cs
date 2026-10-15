namespace ViloesApi.Domain.Common.Validacoes;

/// <remarks>
/// Valores nulos são ignorados; a obrigatoriedade é verificada com <c>NotNull</c>.
/// </remarks>
public static partial class ValidationExtensions
{
    public static List<ValidacaoFalha> Id(
        this List<ValidacaoFalha> source,
        int? value,
        string propertyName)
        => source
            .NotNull(value, propertyName, $"O id '{propertyName}' não foi informado.")
            .GreaterThan(value, 0, propertyName, $"O id '{propertyName}' tem valor inválido ({value}).");

    public static List<ValidacaoFalha> GreaterThan(
        this List<ValidacaoFalha> source,
        int? value,
        int comparer,
        string propertyName)
        => GreaterThan(
            source,
            value,
            comparer,
            propertyName,
            $"O campo '{propertyName}' deve ter valor superior a '{comparer}'.");

    public static List<ValidacaoFalha> GreaterThan(
        this List<ValidacaoFalha> source,
        int? value,
        int comparer,
        string propertyName,
        string message)
    {
        if (value is null || value > comparer) { return source; }

        return source.Add(propertyName, message);
    }

    public static List<ValidacaoFalha> GreaterOrEqualsTo(
        this List<ValidacaoFalha> source,
        int? value,
        int comparer,
        string propertyName)
        => GreaterOrEqualsTo(
            source,
            value,
            comparer,
            propertyName,
            $"O campo '{propertyName}' deve ter valor igual ou superior a '{comparer}'.");

    public static List<ValidacaoFalha> GreaterOrEqualsTo(
        this List<ValidacaoFalha> source,
        int? value,
        int comparer,
        string propertyName,
        string message)
    {
        if (value is null || value >= comparer) { return source; }

        return source.Add(propertyName, message);
    }

    public static List<ValidacaoFalha> LowerOrEqualsTo(
        this List<ValidacaoFalha> source,
        int? value,
        int comparer,
        string propertyName)
        => LowerOrEqualsTo(
            source,
            value,
            comparer,
            propertyName,
            $"O campo '{propertyName}' deve ter valor igual ou inferior a '{comparer}'.");

    public static List<ValidacaoFalha> LowerOrEqualsTo(
        this List<ValidacaoFalha> source,
        int? value,
        int comparer,
        string propertyName,
        string message)
    {
        if (value is null || value <= comparer) { return source; }

        return source.Add(propertyName, message);
    }
}
