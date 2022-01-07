using Pockybot.Core.Interfaces;

namespace Pockybot.Core.Entities
{
    public abstract class BaseCacheableEntity : ICacheableEntity
    {
        public ulong EntityId { get; set; }

        public ulong OwnerId { get; set; }
        public long ExpiresAt { get; set; }

        public bool IsExpired => DateTimeOffset.UtcNow >= DateTimeOffset.FromUnixTimeMilliseconds(ExpiresAt);
    }
}
