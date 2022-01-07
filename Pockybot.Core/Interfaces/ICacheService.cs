namespace Pockybot.Core.Interfaces
{
    public interface ICacheService<T> where T : ICacheableEntity
    {
        Dictionary<ulong, T> Cache { get; }

        IEnumerable<T> Entities { get; }

        bool AddEntity(T entity);

        T? GetEntity(ulong entityId);

        IEnumerable<T> GetEntitiesByOwner(ulong ownerId);

        bool RemoveEntity(ulong entityId);

        int RemoveEntitiesbyOwner(ulong ownerId);

        int RemoveExpiredEntities();

        bool HasEntity(ulong entityId);
    }
}
