namespace ViloesApi.Application.Common.Exceptions;

/// <summary>
/// Lançada quando o recurso identificado na requisição não existe.
/// </summary>
public class NaoEncontradoException(string message) : Exception(message);
