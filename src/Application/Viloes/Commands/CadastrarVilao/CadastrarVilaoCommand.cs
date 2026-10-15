using ViloesApi.Domain.Entities.Viloes;
using ViloesApi.Domain.Entities.Viloes.Models;

namespace ViloesApi.Application.Viloes.Commands.CadastrarVilao;

public class CadastrarVilaoCommand : IRequest<int>
{
    public string Nome { get; set; }

    public string Franquia { get; set; }

    public string Ator { get; set; }

    public int Ano { get; set; }

    public decimal Maldade { get; set; }
}

public class CadastrarVilaoCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CadastrarVilaoCommand, int>
{
    public async Task<int> Handle(CadastrarVilaoCommand request, CancellationToken cancellationToken)
    {
        var vilao = new Vilao(new VilaoNovoModel
        {
            Nome = request.Nome,
            Franquia = request.Franquia,
            Ator = request.Ator,
            Ano = request.Ano,
            Maldade = request.Maldade,
        });

        unitOfWork.GetRepository<Vilao>().Add(vilao);

        await unitOfWork.CommitAsync(cancellationToken);

        return vilao.Id;
    }
}
