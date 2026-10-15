using System.Text.Json.Serialization;
using ViloesApi.Domain.Entities.Viloes;
using ViloesApi.Domain.Entities.Viloes.Models;

namespace ViloesApi.Application.Viloes.Commands.AlterarDadosBasicosVilao;

public class AlterarDadosBasicosVilaoCommand : IRequest
{
    [JsonIgnore]
    public int VilaoId { get; set; }

    public string Nome { get; set; }

    public int Ano { get; set; }
}

public class AlterarDadosBasicosVilaoCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<AlterarDadosBasicosVilaoCommand>
{
    public async Task Handle(AlterarDadosBasicosVilaoCommand request, CancellationToken cancellationToken)
    {
        var vilao = await unitOfWork
            .GetRepository<Vilao>()
            .FindBy(v => v.Id == request.VilaoId)
            .Include(v => v.Aparicoes)
            .FirstAsync(cancellationToken);

        vilao.AlterarDadosBasicos(new VilaoDadosBasicosModel
        {
            Nome = request.Nome,
            Ano = request.Ano,
        });

        await unitOfWork.CommitAsync(cancellationToken);
    }
}
