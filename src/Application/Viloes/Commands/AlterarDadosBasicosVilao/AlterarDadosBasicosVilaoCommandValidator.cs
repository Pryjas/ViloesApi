using ViloesApi.Domain.Entities.Viloes;

namespace ViloesApi.Application.Viloes.Commands.AlterarDadosBasicosVilao;

public class AlterarDadosBasicosVilaoCommandValidator : ValidatorBase<AlterarDadosBasicosVilaoCommand>
{
    public AlterarDadosBasicosVilaoCommandValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        RuleFor(c => c.VilaoId)
            .Identificador()
            .MustExists(unitOfWork.GetRepository<Vilao>());

        RuleFor(c => c.Nome).NotEmpty();

        RuleFor(c => c.Ano).NotEmpty();
    }
}
