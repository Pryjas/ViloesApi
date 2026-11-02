namespace ViloesApi.Application.Viloes.Commands.CadastrarVilao;

public class CadastrarVilaoCommandValidator : AbstractValidator<CadastrarVilaoCommand>
{
    public CadastrarVilaoCommandValidator()
    {
        RuleFor(c => c.Nome).NotEmpty();

        RuleFor(c => c.Franquia).NotEmpty();

        RuleFor(c => c.Ator).NotEmpty();

        RuleFor(c => c.Ano).NotEmpty();
        
        RuleFor(c => c.Maldade).GreaterThan(0);
    }
}
