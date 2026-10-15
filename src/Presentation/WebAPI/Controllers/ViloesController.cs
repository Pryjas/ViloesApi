using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViloesApi.Application.Viloes.Commands.AlterarAtorVilao;
using ViloesApi.Application.Viloes.Commands.AlterarDadosBasicosVilao;
using ViloesApi.Application.Viloes.Commands.AlterarFranquiaVilao;
using ViloesApi.Application.Viloes.Commands.AlterarMaldadeVilao;
using ViloesApi.Application.Viloes.Commands.CadastrarVilao;
using ViloesApi.Application.Viloes.Commands.RemoverVilao;
using ViloesApi.Application.Viloes.Queries.GetVilao;
using ViloesApi.Application.Viloes.Queries.GetViloes;
using ViloesApi.Infra.Hosting.Wrappers;

namespace ViloesApi.WebAPI.Controllers;

public class ViloesController(ISender mediator) : ApiController
{
    [HttpGet]
    [EndpointSummary("Lista todos os vilões.")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetViloesQuery(), cancellationToken);
        return Ok(new Response(result));
    }

    [HttpGet("{vilaoId:int}")]
    [EndpointSummary("Busca um vilão pelo identificador.")]
    public async Task<IActionResult> GetAsync([FromRoute] int vilaoId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetVilaoQuery { VilaoId = vilaoId }, cancellationToken);
        return Ok(new Response(result));
    }

    [HttpPost]
    [EndpointSummary("Cadastra um novo vilão.")]
    public async Task<IActionResult> PostAsync(
        [FromBody] CadastrarVilaoCommand command,
        CancellationToken cancellationToken)
    {
        var vilaoId = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(
            nameof(GetAsync),
            new { vilaoId },
            new Response(vilaoId, "Vilão cadastrado com sucesso."));
    }

    [HttpPut("{vilaoId:int}/dados-basicos")]
    [EndpointSummary("Altera o nome e o ano de estreia do vilão.")]
    public async Task<IActionResult> PutDadosBasicosAsync(
        [FromRoute] int vilaoId,
        [FromBody] AlterarDadosBasicosVilaoCommand command,
        CancellationToken cancellationToken)
    {
        command.VilaoId = vilaoId;
        await mediator.Send(command, cancellationToken);
        return Ok(new Response(null, "Dados básicos do vilão alterados com sucesso."));
    }

    [HttpPut("{vilaoId:int}/maldade")]
    [EndpointSummary("Altera o nível de maldade do vilão.")]
    public async Task<IActionResult> PutMaldadeAsync(
        [FromRoute] int vilaoId,
        [FromBody] AlterarMaldadeVilaoCommand command,
        CancellationToken cancellationToken)
    {
        command.VilaoId = vilaoId;
        await mediator.Send(command, cancellationToken);
        return Ok(new Response(null, "Maldade do vilão alterada com sucesso."));
    }

    [HttpPut("{vilaoId:int}/ator")]
    [EndpointSummary("Altera o ator ou atriz que interpreta o vilão.")]
    public async Task<IActionResult> PutAtorAsync(
        [FromRoute] int vilaoId,
        [FromBody] AlterarAtorVilaoCommand command,
        CancellationToken cancellationToken)
    {
        command.VilaoId = vilaoId;
        await mediator.Send(command, cancellationToken);
        return Ok(new Response(null, "Ator do vilão alterado com sucesso."));
    }

    [HttpPut("{vilaoId:int}/franquia")]
    [EndpointSummary("Altera a franquia do vilão.")]
    public async Task<IActionResult> PutFranquiaAsync(
        [FromRoute] int vilaoId,
        [FromBody] AlterarFranquiaVilaoCommand command,
        CancellationToken cancellationToken)
    {
        command.VilaoId = vilaoId;
        await mediator.Send(command, cancellationToken);
        return Ok(new Response(null, "Franquia do vilão alterada com sucesso."));
    }

    [HttpDelete("{vilaoId:int}")]
    [EndpointSummary("Remove o vilão e suas aparições.")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int vilaoId, CancellationToken cancellationToken)
    {
        await mediator.Send(new RemoverVilaoCommand { VilaoId = vilaoId }, cancellationToken);
        return Ok(new Response(null, "Vilão removido com sucesso."));
    }
}
