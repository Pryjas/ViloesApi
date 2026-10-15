using ViloesApi.Domain.Common.Validacoes;
using ViloesApi.Domain.Entities.Viloes.Models;

namespace ViloesApi.Domain.Entities.Viloes;

public partial class Aparicao : BaseEntity<int>
{
    internal Aparicao(AparicaoNovaModel model)
    {
        Guard.NotNull(model, nameof(model));
        Guard.Enforce(PodeCriar(model));

        Titulo = model.Titulo.Trim();
        Tipo = model.Tipo;
        Ano = model.Ano;
        Temporada = model.Temporada;
    }

    private Aparicao()
    {
        // necessário para o EF
    }

    public int VilaoId { get; private set; }

    public string Titulo { get; private set; }

    public TipoObra Tipo { get; private set; }

    public int Ano { get; private set; }

    public int? Temporada { get; private set; }
}
