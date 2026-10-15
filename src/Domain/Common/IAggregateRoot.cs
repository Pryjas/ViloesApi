namespace ViloesApi.Domain.Common;

/// <summary>
/// Raiz de um agregado. Repositórios só consultam raízes, as entidades filhas são acessadas por elas.
/// </summary>
public interface IAggregateRoot;
