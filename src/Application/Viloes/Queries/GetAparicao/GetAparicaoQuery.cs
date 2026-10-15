using ViloesApi.Domain.Entities.Viloes;

namespace ViloesApi.Application.Viloes.Queries.GetAparicao;

public class GetAparicaoQuery : IRequest<GetAparicaoDto>
{
    public int VilaoId { get; set; }

    public int AparicaoId { get; set; }
}

public class GetAparicaoQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAparicaoQuery, GetAparicaoDto>
{
    public async Task<GetAparicaoDto> Handle(GetAparicaoQuery request, CancellationToken cancellationToken)
        => await unitOfWork
            .GetRepository<Vilao>()
            .FindBy(v => v.Id == request.VilaoId)
            .AsNoTracking()
            .SelectMany(v => v.Aparicoes)
            .Where(a => a.Id == request.AparicaoId)
            .Select(a => new GetAparicaoDto
            {
                AparicaoId = a.Id,
                VilaoId = a.VilaoId,
                Titulo = a.Titulo,
                Tipo = a.Tipo,
                Ano = a.Ano,
                Temporada = a.Temporada,
            })
            .FirstOrDefaultAsync(cancellationToken);
}
