using ViloesApi.Domain.Entities.Viloes;

namespace ViloesApi.Application.Viloes.Commands.AlterarFranquiaVilao;

public class AlterarFranquiaVilaoCommandValidator : ValidatorBase<AlterarFranquiaVilaoCommand>
{
    public AlterarFranquiaVilaoCommandValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        RuleFor(c => c.VilaoId)
            .Identificador()
            .MustExists(unitOfWork.GetRepository<Vilao>());

        RuleFor(c => c.Franquia).NotEmpty();
    }
}
