using ViloesApi.Domain.Entities.Viloes;

namespace ViloesApi.Application.Viloes.Commands.AlterarAtorVilao;

public class AlterarAtorVilaoCommandValidator : ValidatorBase<AlterarAtorVilaoCommand>
{
    public AlterarAtorVilaoCommandValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        RuleFor(c => c.VilaoId)
            .Identificador()
            .MustExists(unitOfWork.GetRepository<Vilao>());

        RuleFor(c => c.Ator).NotEmpty();
    }
}
