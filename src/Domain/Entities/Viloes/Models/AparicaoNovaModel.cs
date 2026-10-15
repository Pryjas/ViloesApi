namespace ViloesApi.Domain.Entities.Viloes.Models;

public class AparicaoNovaModel
{
    public string Titulo { get; set; }

    public TipoObra Tipo { get; set; }

    public int Ano { get; set; }

    public int? Temporada { get; set; }
}
