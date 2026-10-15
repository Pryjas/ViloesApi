using ViloesApi.Domain.Entities.Viloes;

namespace ViloesApi.Application.Viloes.Commands.RemoverVilao;

public class RemoverVilaoCommand : IRequest
{
    public int VilaoId { get; set; }
}

public class RemoverVilaoCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<RemoverVilaoCommand>
{
    public async Task Handle(RemoverVilaoCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Vilao>();

        var vilao = await repository
            .FindBy(v => v.Id == request.VilaoId)
            .Include(v => v.Aparicoes)
            .FirstAsync(cancellationToken);

        repository.Delete(vilao);

        await unitOfWork.CommitAsync(cancellationToken);
    }
}
