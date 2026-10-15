using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViloesApi.Application.Viloes.Commands.AdicionarAparicao;
using ViloesApi.Application.Viloes.Queries.GetAparicao;
using ViloesApi.Application.Viloes.Queries.GetAparicoesVilao;
using ViloesApi.Infra.Hosting.Wrappers;

namespace ViloesApi.WebAPI.Controllers;

/// <remarks>
/// Aparição é sub-recurso do vilão (entidade filha do agregado), por isso a rota fica aninhada.
/// </remarks>
[Route("api/viloes/{vilaoId:int}/aparicoes")]
public class AparicoesController(ISender mediator) : ApiController
{
    [HttpGet]
    [EndpointSummary("Lista as aparições de um vilão.")]
    public async Task<IActionResult> GetAllAsync([FromRoute] int vilaoId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAparicoesVilaoQuery { VilaoId = vilaoId }, cancellationToken);
        return Ok(new Response(result));
    }

    [HttpGet("{aparicaoId:int}")]
    [EndpointSummary("Busca uma aparição do vilão pelo identificador.")]
    public async Task<IActionResult> GetAsync(
        [FromRoute] int vilaoId,
        [FromRoute] int aparicaoId,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new GetAparicaoQuery { VilaoId = vilaoId, AparicaoId = aparicaoId },
            cancellationToken);
        return Ok(new Response(result));
    }

    [HttpPost]
    [EndpointSummary("Adiciona uma aparição (filme ou série) ao vilão.")]
    public async Task<IActionResult> PostAsync(
        [FromRoute] int vilaoId,
        [FromBody] AdicionarAparicaoCommand command,
        CancellationToken cancellationToken)
    {
        command.VilaoId = vilaoId;
        var aparicaoId = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(
            nameof(GetAsync),
            new { vilaoId, aparicaoId },
            new Response(aparicaoId, "Aparição cadastrada com sucesso."));
    }
}
