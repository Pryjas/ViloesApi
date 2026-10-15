namespace ViloesApi.Domain.Common.Validacoes;

/// <remarks>
/// Tamanhos são medidos sem os espaços das pontas, pois é assim que o texto é gravado.
/// Valores nulos ou em branco são ignorados; a obrigatoriedade é verificada com <c>NotNullOrWhiteSpace</c>.
/// </remarks>
public static partial class ValidationExtensions
{
    public static List<ValidacaoFalha> NotNullOrWhiteSpace(
        this List<ValidacaoFalha> source,
        string value,
        string propertyName)
        => NotNullOrWhiteSpace(source, value, propertyName, $"O campo '{propertyName}' não foi informado.");

    public static List<ValidacaoFalha> NotNullOrWhiteSpace(
        this List<ValidacaoFalha> source,
        string value,
        string propertyName,
        string message)
    {
        if (!string.IsNullOrWhiteSpace(value)) { return source; }

        return source.Add(propertyName, message);
    }

    public static List<ValidacaoFalha> MinimumLength(
        this List<ValidacaoFalha> source,
        string value,
        int minimumLength,
        string propertyName)
        => MinimumLength(
            source,
            value,
            minimumLength,
            propertyName,
            $"O campo '{propertyName}' deve conter no mínimo '{minimumLength}' caracteres.");

    public static List<ValidacaoFalha> MinimumLength(
        this List<ValidacaoFalha> source,
        string value,
        int minimumLength,
        string propertyName,
        string message)
    {
        if (string.IsNullOrWhiteSpace(value)) { return source; }
        if (value.Trim().Length >= minimumLength) { return source; }

        return source.Add(propertyName, message);
    }

    public static List<ValidacaoFalha> MaximumLength(
        this List<ValidacaoFalha> source,
        string value,
        int maximumLength,
        string propertyName)
        => MaximumLength(
            source,
            value,
            maximumLength,
            propertyName,
            $"O campo '{propertyName}' deve conter no máximo '{maximumLength}' caracteres.");

    public static List<ValidacaoFalha> MaximumLength(
        this List<ValidacaoFalha> source,
        string value,
        int maximumLength,
        string propertyName,
        string message)
    {
        if (value is null) { return source; }
        if (value.Trim().Length <= maximumLength) { return source; }

        return source.Add(propertyName, message);
    }
}
