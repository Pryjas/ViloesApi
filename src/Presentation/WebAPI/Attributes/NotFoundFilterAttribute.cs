using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Filters;
using ViloesApi.Infra.Hosting.Wrappers;

namespace ViloesApi.WebAPI.Attributes;

/// <summary>
/// Um filtro para transformar o status code de um resultado vazio em 404 (NotFound).
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class NotFoundFilterAttribute : Attribute, IAlwaysRunResultFilter
{
    public void OnResultExecuted(ResultExecutedContext context)
    {
        // Tratamento realizado no OnResultExecuting
    }

    public void OnResultExecuting(ResultExecutingContext context)
    {
        var actions = context.ActionDescriptor.ActionConstraints;

        if (actions is null || actions.Count is 0) { return; }

        var action = actions.First();

        if (action is not HttpMethodActionConstraint httpMethod) { return; }

        if (!httpMethod.HttpMethods.Contains("GET")) { return; }

        if (context.Result is not ObjectResult { Value: Response { Data: null, Succeeded: true } response }) { return; }

        response.Succeeded = false;

        if (string.IsNullOrWhiteSpace(response.Message))
        {
            response.Message = "Registro não encontrado.";
        }

        context.Result = new ObjectResult(response)
        {
            StatusCode = StatusCodes.Status404NotFound,
        };
    }
}
