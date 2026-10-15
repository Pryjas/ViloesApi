using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using ViloesApi.Domain.Exceptions;
using ViloesApi.Infra.Hosting.Extensions;
using ViloesApi.Infra.Hosting.Wrappers;

namespace ViloesApi.Infra.Hosting.ExceptionHandlers;

/// <summary>
/// Regra de negócio do domínio violada: 422 Unprocessable Entity
/// (a requisição está bem formada, mas os valores não são aceitos).
/// </summary>
public class ValidacaoExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not ValidacaoException validacaoException)
        {
            return false;
        }

        var result = new Response
        {
            Succeeded = false,
            Message = "Uma ou mais regras de negócio foram violadas.",
            Errors = validacaoException.Falhas.ComNomesDoJson(),
        };
        httpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
        await httpContext.Response.WriteAsJsonAsync(result, cancellationToken);
        return true;
    }
}
