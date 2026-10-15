using System.Text.Json.Serialization;
using ViloesApi.Domain.Entities.Viloes;

namespace ViloesApi.Application.Viloes.Commands.AlterarMaldadeVilao;

public class AlterarMaldadeVilaoCommand : IRequest
{
    [JsonIgnore]
    public int VilaoId { get; set; }

    public decimal Maldade { get; set; }
}

public class AlterarMaldadeVilaoCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<AlterarMaldadeVilaoCommand>
{
    public async Task Handle(AlterarMaldadeVilaoCommand request, CancellationToken cancellationToken)
    {
        var vilao = await unitOfWork
            .GetRepository<Vilao>()
            .FindBy(v => v.Id == request.VilaoId)
            .FirstAsync(cancellationToken);

        vilao.AlterarMaldade(request.Maldade);

        await unitOfWork.CommitAsync(cancellationToken);
    }
}
