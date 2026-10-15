namespace ViloesApi.Application.Common.Validators;

public static class ValidatorErrorCodes
{
    /// <summary>
    /// Código das falhas de registro inexistente. O ValidationBehaviour converte essas falhas em 404.
    /// </summary>
    public const string NaoEncontrado = "NaoEncontrado";
}
