using System.Text.Json.Serialization;
using ViloesApi.Domain.Entities.Viloes;

namespace ViloesApi.Application.Viloes.Commands.AlterarFranquiaVilao;

public class AlterarFranquiaVilaoCommand : IRequest
{
    [JsonIgnore]
    public int VilaoId { get; set; }

    public string Franquia { get; set; }
}

public class AlterarFranquiaVilaoCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<AlterarFranquiaVilaoCommand>
{
    public async Task Handle(AlterarFranquiaVilaoCommand request, CancellationToken cancellationToken)
    {
        var vilao = await unitOfWork
            .GetRepository<Vilao>()
            .FindBy(v => v.Id == request.VilaoId)
            .FirstAsync(cancellationToken);

        vilao.AlterarFranquia(request.Franquia);

        await unitOfWork.CommitAsync(cancellationToken);
    }
}
