namespace Pockybot.Core.Interfaces
{
    public interface ICacheableEntity
    {
        ulong EntityId { get; set; }

        ulong OwnerId { get; set; }

        long ExpiresAt { get; set; }

        bool IsExpired { get; }

    }
}
