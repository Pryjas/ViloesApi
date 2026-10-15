using System.Text.Json.Serialization;
using ViloesApi.Domain.Entities.Viloes;
using ViloesApi.Domain.Entities.Viloes.Models;

namespace ViloesApi.Application.Viloes.Commands.AdicionarAparicao;

public class AdicionarAparicaoCommand : IRequest<int>
{
    [JsonIgnore]
    public int VilaoId { get; set; }

    public string Titulo { get; set; }

    public TipoObra Tipo { get; set; }

    public int Ano { get; set; }

    public int? Temporada { get; set; }
}

public class AdicionarAparicaoCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<AdicionarAparicaoCommand, int>
{
    public async Task<int> Handle(AdicionarAparicaoCommand request, CancellationToken cancellationToken)
    {
        var vilao = await unitOfWork
            .GetRepository<Vilao>()
            .FindBy(v => v.Id == request.VilaoId)
            .Include(v => v.Aparicoes)
            .FirstAsync(cancellationToken);

        var aparicao = vilao.AdicionarAparicao(new AparicaoNovaModel
        {
            Titulo = request.Titulo,
            Tipo = request.Tipo,
            Ano = request.Ano,
            Temporada = request.Temporada,
        });

        await unitOfWork.CommitAsync(cancellationToken);

        return aparicao.Id;
    }
}
