namespace BMS.Company.Domain;

// this file usually goes into shared kernel to be used by any domain
// right now SharedKernel also has full MediatR library, so not referencing it to
// make dependency tree light for domain project
public interface IEntity
{
    Guid Guid { get; }
}

public class Entity : IEntity
{
    public Guid Guid { get; protected init; } = Guid.NewGuid();
    public override bool Equals(object? obj)
    {
        return (obj as IEntity)?.Guid == Guid;
    }

    protected bool Equals(Entity other)
    {
        return Guid.Equals(other.Guid);
    }

    public override int GetHashCode()
    {
        return Guid.GetHashCode();
    }
}