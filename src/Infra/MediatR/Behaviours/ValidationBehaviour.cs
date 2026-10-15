using FluentValidation;
using MediatR;
using ViloesApi.Application.Common.Exceptions;
using ViloesApi.Application.Common.Validators;
using ViloesApi.Domain.Common.Validacoes;

namespace ViloesApi.Infra.MediatR.Behaviours;

/// <summary>
/// Executa os validators do FluentValidation antes do handler de cada Command/Query.
/// </summary>
public class ValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any()) { return await next(); }

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var falhas = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f is not null)
            .ToList();

        if (falhas.Count is 0) { return await next(); }

        // Recurso da rota inexistente tem precedência: não adianta corrigir os campos de algo que não existe.
        var naoEncontrado = falhas.FirstOrDefault(f => f.ErrorCode == ValidatorErrorCodes.NaoEncontrado);
        if (naoEncontrado is not null)
        {
            throw new NaoEncontradoException(naoEncontrado.ErrorMessage);
        }

        throw new RequisicaoInvalidaException(
            falhas.Select(f => new ValidacaoFalha(f.PropertyName, f.ErrorMessage)));
    }
}
