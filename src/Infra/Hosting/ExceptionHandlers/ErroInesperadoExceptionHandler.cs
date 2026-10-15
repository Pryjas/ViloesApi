using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ViloesApi.Infra.Hosting.Wrappers;

namespace ViloesApi.Infra.Hosting.ExceptionHandlers;

/// <summary>
/// Qualquer exceção não tratada pelos handlers anteriores: 500 Internal Server Error, sem expor detalhes.
/// </summary>
public class ErroInesperadoExceptionHandler(ILogger<ErroInesperadoExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Erro inesperado ao processar {Metodo} {Rota}.", httpContext.Request.Method, httpContext.Request.Path);

        var result = new Response
        {
            Succeeded = false,
            Message = "Ocorreu um erro inesperado.",
        };
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(result, cancellationToken);
        return true;
    }
}
