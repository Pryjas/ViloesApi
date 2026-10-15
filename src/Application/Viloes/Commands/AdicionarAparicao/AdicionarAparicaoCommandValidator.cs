using ViloesApi.Domain.Entities.Viloes;

namespace ViloesApi.Application.Viloes.Commands.AdicionarAparicao;

public class AdicionarAparicaoCommandValidator : ValidatorBase<AdicionarAparicaoCommand>
{
    public AdicionarAparicaoCommandValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        RuleFor(c => c.VilaoId)
            .Identificador()
            .MustExists(unitOfWork.GetRepository<Vilao>());

        RuleFor(c => c.Titulo).NotEmpty();

        RuleFor(c => c.Tipo).NotEmpty();

        RuleFor(c => c.Ano).NotEmpty();
    }
}
