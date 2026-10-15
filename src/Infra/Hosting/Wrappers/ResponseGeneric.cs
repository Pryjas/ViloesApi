using ViloesApi.Domain.Common.Validacoes;

namespace ViloesApi.Infra.Hosting.Wrappers;

public abstract class ResponseGeneric<T>
{
    public T Data { get; set; }

    public bool Succeeded { get; set; }

    public IReadOnlyCollection<ValidacaoFalha> Errors { get; init; }

    public string Message { get; set; } = string.Empty;
}
