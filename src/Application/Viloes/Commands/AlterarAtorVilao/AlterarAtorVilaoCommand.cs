using System.Text.Json.Serialization;
using ViloesApi.Domain.Entities.Viloes;

namespace ViloesApi.Application.Viloes.Commands.AlterarAtorVilao;

public class AlterarAtorVilaoCommand : IRequest
{
    [JsonIgnore]
    public int VilaoId { get; set; }

    public string Ator { get; set; }
}

public class AlterarAtorVilaoCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<AlterarAtorVilaoCommand>
{
    public async Task Handle(AlterarAtorVilaoCommand request, CancellationToken cancellationToken)
    {
        var vilao = await unitOfWork
            .GetRepository<Vilao>()
            .FindBy(v => v.Id == request.VilaoId)
            .FirstAsync(cancellationToken);

        vilao.AlterarAtor(request.Ator);

        await unitOfWork.CommitAsync(cancellationToken);
    }
}
