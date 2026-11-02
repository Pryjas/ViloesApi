using ViloesApi.Domain.Entities.Viloes;

namespace ViloesApi.Application.Viloes.Commands.AlterarMaldadeVilao;

public class AlterarMaldadeVilaoCommandValidator : ValidatorBase<AlterarMaldadeVilaoCommand>
{
    public AlterarMaldadeVilaoCommandValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        RuleFor(c => c.VilaoId)
            .Identificador()
            .MustExists(unitOfWork.GetRepository<Vilao>());
        
        RuleFor(c => c.Maldade).GreaterThan(0);
    }
}
