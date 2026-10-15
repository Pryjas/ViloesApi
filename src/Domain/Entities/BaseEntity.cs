namespace ViloesApi.Domain.Entities;

public abstract class BaseEntity<TKeyType>
{
    public TKeyType Id { get; protected set; }
}
