using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using ViloesApi.Application.Common.Exceptions;
using ViloesApi.Infra.Hosting.Extensions;
using ViloesApi.Infra.Hosting.Wrappers;

namespace ViloesApi.Infra.Hosting.ExceptionHandlers;

/// <summary>
/// Campos esperados ausentes ou inválidos na requisição (FluentValidation): 400 Bad Request.
/// </summary>
public class RequisicaoInvalidaExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not RequisicaoInvalidaException requisicaoInvalidaException)
        {
            return false;
        }

        var result = new Response
        {
            Succeeded = false,
            Message = "Falha ao validar os campos.",
            Errors = requisicaoInvalidaException.Falhas.ComNomesDoJson(),
        };
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await httpContext.Response.WriteAsJsonAsync(result, cancellationToken);
        return true;
    }
}
