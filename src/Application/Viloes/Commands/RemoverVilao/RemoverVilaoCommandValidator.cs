using ViloesApi.Domain.Entities.Viloes;

namespace ViloesApi.Application.Viloes.Commands.RemoverVilao;

public class RemoverVilaoCommandValidator : ValidatorBase<RemoverVilaoCommand>
{
    public RemoverVilaoCommandValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        RuleFor(c => c.VilaoId)
            .Identificador()
            .MustExists(unitOfWork.GetRepository<Vilao>());
    }
}
