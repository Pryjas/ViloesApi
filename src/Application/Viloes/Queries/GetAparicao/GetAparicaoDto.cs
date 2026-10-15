using ViloesApi.Domain.Entities.Viloes;

namespace ViloesApi.Application.Viloes.Queries.GetAparicao;

public class GetAparicaoDto
{
    public int AparicaoId { get; set; }

    public int VilaoId { get; set; }

    public string Titulo { get; set; }

    public TipoObra Tipo { get; set; }

    public int Ano { get; set; }

    public int? Temporada { get; set; }
}
