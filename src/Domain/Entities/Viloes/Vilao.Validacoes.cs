using ViloesApi.Domain.Common.Validacoes;
using ViloesApi.Domain.Entities.Viloes.Models;

namespace ViloesApi.Domain.Entities.Viloes;

public partial class Vilao
{
    private static List<ValidacaoFalha> PodeCriar(VilaoNovoModel model)
        => Validacao.Init
            .Join(NomeValido(model.Nome))
            .Join(AnoValido(model.Ano))
            .Join(FranquiaValida(model.Franquia))
            .Join(AtorValido(model.Ator))
            .Join(MaldadeValida(model.Maldade));

    private List<ValidacaoFalha> PodeAlterarDadosBasicos(VilaoDadosBasicosModel model)
        => Validacao.Init
            .Join(NomeValido(model.Nome))
            .Join(AnoValido(model.Ano))
            .NotContainsAny(
                _aparicoes,
                a => a.Ano < model.Ano,
                nameof(model.Ano),
                $"O ano de estreia não pode ser posterior à primeira aparição já cadastrada ({_aparicoes.Min(a => (int?)a.Ano)}).");

    private List<ValidacaoFalha> PodeAlterarMaldade(decimal maldade)
        => Validacao.Init
            .Join(MaldadeValida(maldade));

    private List<ValidacaoFalha> PodeAlterarAtor(string ator)
        => Validacao.Init
            .Join(AtorValido(ator));

    private List<ValidacaoFalha> PodeAlterarFranquia(string franquia)
        => Validacao.Init
            .Join(FranquiaValida(franquia));

    private List<ValidacaoFalha> PodeAdicionarAparicao(AparicaoNovaModel model)
        => Validacao.Init
            .GreaterOrEqualsTo(
                model.Ano,
                Ano,
                nameof(model.Ano),
                $"A aparição não pode ser anterior à estreia do vilão ({Ano}).")
            .NotContainsAny(
                _aparicoes,
                a => a.Tipo == model.Tipo
                     && a.Temporada == model.Temporada
                     && string.Equals(a.Titulo, model.Titulo?.Trim(), StringComparison.OrdinalIgnoreCase),
                nameof(model.Titulo),
                "Esta aparição já está cadastrada para o vilão.");

    private static List<ValidacaoFalha> NomeValido(string nome)
        => Validacao.Init
            .NotNullOrWhiteSpace(nome, nameof(Nome))
            .MinimumLength(nome, 2, nameof(Nome))
            .MaximumLength(nome, 100, nameof(Nome));

    private static List<ValidacaoFalha> AnoValido(int ano)
        => Validacao.Init
            .GreaterOrEqualsTo(ano, 1888, nameof(Ano))
            .LowerOrEqualsTo(ano, 2100, nameof(Ano));

    private static List<ValidacaoFalha> FranquiaValida(string franquia)
        => Validacao.Init
            .NotNullOrWhiteSpace(franquia, nameof(Franquia))
            .MinimumLength(franquia, 2, nameof(Franquia))
            .MaximumLength(franquia, 100, nameof(Franquia));

    private static List<ValidacaoFalha> AtorValido(string ator)
        => Validacao.Init
            .NotNullOrWhiteSpace(ator, nameof(Ator))
            .MinimumLength(ator, 2, nameof(Ator))
            .MaximumLength(ator, 150, nameof(Ator));

    private static List<ValidacaoFalha> MaldadeValida(decimal maldade)
        => Validacao.Init
            .GreaterThan(maldade, 0m, nameof(Maldade))
            .LowerOrEqualsTo(maldade, 10m, nameof(Maldade));
}
