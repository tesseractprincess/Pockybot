namespace Pockybot.Core.Entities
{
    public sealed class UserMessageEntity : BaseCacheableEntity
    {
        public ulong MessageId => EntityId;

        public ulong UserId => OwnerId;

        public ulong ChannelId { get; set; }

        public ulong GuildId { get; set; }

        public string Content { get; set; } = "";
    }
}
