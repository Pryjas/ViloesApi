using ViloesApi.Domain.Common.Validacoes;
using ViloesApi.Domain.Entities.Viloes.Models;

namespace ViloesApi.Domain.Entities.Viloes;

public partial class Aparicao
{
    private static List<ValidacaoFalha> PodeCriar(AparicaoNovaModel model)
        => Validacao.Init
            .NotNullOrWhiteSpace(model.Titulo, nameof(model.Titulo))
            .MinimumLength(model.Titulo, 1, nameof(model.Titulo))
            .MaximumLength(model.Titulo, 200, nameof(model.Titulo))
            .EnumValue(model.Tipo, nameof(model.Tipo))
            .LowerOrEqualsTo(model.Ano, 2100, nameof(model.Ano))
            .JoinWhen(
                model.Tipo is TipoObra.Serie,
                v => v
                    .NotNull(model.Temporada, nameof(model.Temporada), "A temporada deve ser informada para séries.")
                    .GreaterThan(model.Temporada, 0, nameof(model.Temporada)))
            .JoinWhen(
                model.Tipo is TipoObra.Filme,
                v => v.Null(model.Temporada, nameof(model.Temporada), "Filmes não possuem temporada."));
}
