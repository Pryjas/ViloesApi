namespace ViloesApi.Application.Viloes.Commands.CadastrarVilao;

/// <remarks>
/// Garante que os campos esperados vieram na requisição. Tamanhos e faixas de valores são regras do domínio.
/// </remarks>
public class CadastrarVilaoCommandValidator : AbstractValidator<CadastrarVilaoCommand>
{
    public CadastrarVilaoCommandValidator()
    {
        RuleFor(c => c.Nome).NotEmpty();

        RuleFor(c => c.Franquia).NotEmpty();

        RuleFor(c => c.Ator).NotEmpty();

        RuleFor(c => c.Ano).NotEmpty();
    }
}
