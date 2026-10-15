using ViloesApi.Domain.Common.Validacoes;
using ViloesApi.Domain.Entities.Viloes.Models;

namespace ViloesApi.Domain.Entities.Viloes;

public partial class Vilao
{
    public void AlterarDadosBasicos(VilaoDadosBasicosModel model)
    {
        Guard.NotNull(model, nameof(model));
        Guard.Enforce(PodeAlterarDadosBasicos(model));

        Nome = model.Nome.Trim();
        Ano = model.Ano;
    }

    public void AlterarMaldade(decimal maldade)
    {
        Guard.Enforce(PodeAlterarMaldade(maldade));

        Maldade = maldade;
    }

    public void AlterarAtor(string ator)
    {
        Guard.Enforce(PodeAlterarAtor(ator));

        Ator = ator.Trim();
    }

    public void AlterarFranquia(string franquia)
    {
        Guard.Enforce(PodeAlterarFranquia(franquia));

        Franquia = franquia.Trim();
    }

    public Aparicao AdicionarAparicao(AparicaoNovaModel model)
    {
        Guard.NotNull(model, nameof(model));
        Guard.Enforce(PodeAdicionarAparicao(model));

        var aparicao = new Aparicao(model);
        _aparicoes.Add(aparicao);
        return aparicao;
    }
}
