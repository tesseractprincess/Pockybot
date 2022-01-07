using Pockybot.Core.Interfaces;

namespace Pockybot.Application.Ephemeral
{
    public abstract class BaseCacheService<T> : ICacheService<T> where T : ICacheableEntity
    {
        public Dictionary<ulong, T> Cache { get; } = new();

        public IEnumerable<T> Entities => Cache.Values;

        public bool AddEntity(T entity)
        {
            entity.ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(2).ToUnixTimeMilliseconds();
            return Cache.TryAdd(entity.EntityId, entity);
        }

        public T? GetEntity(ulong entityId)
        {
            if (Cache.TryGetValue(entityId, out var entity))
                return entity;
            else return default;
        }

        public IEnumerable<T> GetEntitiesByOwner(ulong ownerId) => Cache.Values.Where(e => e.OwnerId == ownerId);

        public bool RemoveEntity(ulong entityId) => Cache.Remove(entityId);

        public int RemoveEntitiesbyOwner(ulong ownerId)
        {
            var entityIds = Cache.Values.Where(e => e.OwnerId == ownerId).Select(e => e.EntityId);

            foreach (var entityId in entityIds)
                RemoveEntity(entityId);

            return entityIds.Count();
        }

        public int RemoveExpiredEntities()
        {
            var expiredIds = Entities.Where(e => e.IsExpired).Select(e => e.EntityId).ToList();

            foreach (var id in expiredIds)
                RemoveEntity(id);

            return expiredIds.Count;
        }

        public bool HasEntity(ulong entityId) => Cache.ContainsKey(entityId);
    }
}
