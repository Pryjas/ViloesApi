namespace ViloesApi.Domain.Common.Validacoes;

public static partial class ValidationExtensions
{
    public static List<ValidacaoFalha> GreaterOrEqualsTo(
        this List<ValidacaoFalha> source,
        decimal value,
        decimal comparer,
        string propertyName)
        => GreaterOrEqualsTo(
            source,
            value,
            comparer,
            propertyName,
            $"O campo '{propertyName}' deve ter valor igual ou superior a '{comparer:0.0}'.");
    
    public static List<ValidacaoFalha> GreaterThan(
        this List<ValidacaoFalha> source,
        decimal value,
        decimal comparer,
        string propertyName)
        => GreaterThan(
            source,
            value,
            comparer,
            propertyName,
            $"O campo '{propertyName}' deve ter valor superior a '{comparer:0.0}'.");

    public static List<ValidacaoFalha> GreaterThan(
        this List<ValidacaoFalha> source,
        decimal value,
        decimal comparer,
        string propertyName,
        string message)
    {
        if (value > comparer) { return source; }

        return source.Add(propertyName, message);
    }

    public static List<ValidacaoFalha> GreaterOrEqualsTo(
        this List<ValidacaoFalha> source,
        decimal value,
        decimal comparer,
        string propertyName,
        string message)
    {
        if (value >= comparer) { return source; }

        return source.Add(propertyName, message);
    }

    public static List<ValidacaoFalha> LowerOrEqualsTo(
        this List<ValidacaoFalha> source,
        decimal value,
        decimal comparer,
        string propertyName)
        => LowerOrEqualsTo(
            source,
            value,
            comparer,
            propertyName,
            $"O campo '{propertyName}' deve ter valor igual ou inferior a '{comparer:0.0}'.");

    public static List<ValidacaoFalha> LowerOrEqualsTo(
        this List<ValidacaoFalha> source,
        decimal value,
        decimal comparer,
        string propertyName,
        string message)
    {
        if (value <= comparer) { return source; }

        return source.Add(propertyName, message);
    }
}
