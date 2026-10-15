using ViloesApi.Domain.Common;
using ViloesApi.Domain.Common.Validacoes;
using ViloesApi.Domain.Entities.Viloes.Models;

namespace ViloesApi.Domain.Entities.Viloes;

public partial class Vilao : BaseEntity<int>, IAggregateRoot
{
    public Vilao(VilaoNovoModel model)
    {
        Guard.NotNull(model, nameof(model));
        Guard.Enforce(PodeCriar(model));

        Nome = model.Nome.Trim();
        Franquia = model.Franquia.Trim();
        Ator = model.Ator.Trim();
        Ano = model.Ano;
        Maldade = model.Maldade;
    }

    private Vilao()
    {
        // necessário para o EF
    }

    public string Nome { get; private set; }

    public string Franquia { get; private set; }

    public string Ator { get; private set; }

    public int Ano { get; private set; }

    public decimal Maldade { get; private set; }

    public IReadOnlyCollection<Aparicao> Aparicoes => _aparicoes.AsReadOnly();
    private readonly List<Aparicao> _aparicoes = [];
}
