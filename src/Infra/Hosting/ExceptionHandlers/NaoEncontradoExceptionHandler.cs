using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using ViloesApi.Application.Common.Exceptions;
using ViloesApi.Infra.Hosting.Wrappers;

namespace ViloesApi.Infra.Hosting.ExceptionHandlers;

/// <summary>
/// Recurso identificado na rota não existe: 404 Not Found.
/// </summary>
public class NaoEncontradoExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not NaoEncontradoException)
        {
            return false;
        }

        var result = new Response
        {
            Succeeded = false,
            Message = exception.Message,
        };
        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
        await httpContext.Response.WriteAsJsonAsync(result, cancellationToken);
        return true;
    }
}
