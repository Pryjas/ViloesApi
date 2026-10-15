using System.Text.Json;
using ViloesApi.Domain.Common.Validacoes;

namespace ViloesApi.Infra.Hosting.Extensions;

public static class ValidacaoFalhaExtensions
{
    /// <summary>
    /// Deixa o nome do campo de cada falha igual ao do JSON (ex.: "Nome" → "nome").
    /// </summary>
    public static List<ValidacaoFalha> ComNomesDoJson(this IEnumerable<ValidacaoFalha> falhas)
        => falhas
            .Select(f => f with { PropertyName = JsonNamingPolicy.CamelCase.ConvertName(f.PropertyName) })
            .ToList();
}
