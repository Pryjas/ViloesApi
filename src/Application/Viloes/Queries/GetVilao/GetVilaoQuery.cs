using ViloesApi.Domain.Entities.Viloes;

namespace ViloesApi.Application.Viloes.Queries.GetVilao;

public class GetVilaoQuery : IRequest<GetVilaoDto>
{
    public int VilaoId { get; set; }
}

public class GetVilaoQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetVilaoQuery, GetVilaoDto>
{
    public async Task<GetVilaoDto> Handle(GetVilaoQuery request, CancellationToken cancellationToken)
        => await unitOfWork
            .GetRepository<Vilao>()
            .FindBy(v => v.Id == request.VilaoId)
            .AsNoTracking()
            .Select(v => new GetVilaoDto
            {
                VilaoId = v.Id,
                Nome = v.Nome,
                Franquia = v.Franquia,
                Ator = v.Ator,
                Ano = v.Ano,
                Maldade = v.Maldade,
                QuantidadeAparicoes = v.Aparicoes.Count,
            })
            .FirstOrDefaultAsync(cancellationToken);
}
