namespace ViloesApi.Domain.Common.Validacoes;

public static partial class ValidationExtensions
{
    public static List<ValidacaoFalha> EnumValue<TEnum>(
        this List<ValidacaoFalha> source,
        TEnum value,
        string propertyName)
        where TEnum : struct, Enum
        => EnumValue(
            source,
            value,
            propertyName,
            $"O campo '{propertyName}' deve ser um dos valores: {string.Join(", ", Enum.GetNames<TEnum>())}.");

    public static List<ValidacaoFalha> EnumValue<TEnum>(
        this List<ValidacaoFalha> source,
        TEnum value,
        string propertyName,
        string message)
        where TEnum : struct, Enum
    {
        if (Enum.IsDefined(value)) { return source; }

        return source.Add(propertyName, message);
    }
}
