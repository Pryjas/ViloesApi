using ViloesApi.Domain.Entities.Viloes;

namespace ViloesApi.Application.Viloes.Queries.GetViloes;

public class GetViloesQuery : IRequest<List<GetViloesDto>>;

public class GetViloesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetViloesQuery, List<GetViloesDto>>
{
    public async Task<List<GetViloesDto>> Handle(GetViloesQuery request, CancellationToken cancellationToken)
        => await unitOfWork
            .GetRepository<Vilao>()
            .GetAll()
            .AsNoTracking()
            .OrderBy(v => v.Nome)
            .Select(v => new GetViloesDto
            {
                VilaoId = v.Id,
                Nome = v.Nome,
                Franquia = v.Franquia,
                Ator = v.Ator,
                Ano = v.Ano,
                Maldade = v.Maldade,
            })
            .ToListAsync(cancellationToken);
}
