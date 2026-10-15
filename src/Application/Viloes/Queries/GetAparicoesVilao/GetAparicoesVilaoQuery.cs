using ViloesApi.Domain.Entities.Viloes;

namespace ViloesApi.Application.Viloes.Queries.GetAparicoesVilao;

public class GetAparicoesVilaoQuery : IRequest<List<GetAparicoesVilaoDto>>
{
    public int VilaoId { get; set; }
}

public class GetAparicoesVilaoQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAparicoesVilaoQuery, List<GetAparicoesVilaoDto>>
{
    public async Task<List<GetAparicoesVilaoDto>> Handle(
        GetAparicoesVilaoQuery request,
        CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Vilao>();

        // Vilão inexistente devolve null (404) em vez de lista vazia (200).
        if (!await repository.ExistsAsync(v => v.Id == request.VilaoId, cancellationToken))
        {
            return null;
        }

        return await repository
            .FindBy(v => v.Id == request.VilaoId)
            .AsNoTracking()
            .SelectMany(v => v.Aparicoes)
            .OrderBy(a => a.Ano)
            .Select(a => new GetAparicoesVilaoDto
            {
                AparicaoId = a.Id,
                Titulo = a.Titulo,
                Tipo = a.Tipo,
                Ano = a.Ano,
                Temporada = a.Temporada,
            })
            .ToListAsync(cancellationToken);
    }
}
