using Discord;
using Discord.WebSocket;

namespace Pockybot.Application.Exceptions
{
    public sealed class MessageDeletedException : BasePockybotException
    {
        public override IDictionary<string, object?> DebugObjects { get; set; }

        public MessageDeletedException(
            Cacheable<IMessage, ulong> cachedMessage, 
            Cacheable<IMessageChannel, ulong> cachedChannel,
            SocketGuildChannel? channel,
            SocketMessage? message) : base("The most likely scenario is a failure to access cache or download data. Check nullables for more info.")
        {
            DebugObjects = new Dictionary<string, object?>
            {
                { nameof(cachedMessage), cachedMessage },
                { nameof(cachedChannel), cachedChannel },
                { nameof(message), message },
                { nameof(channel), channel },
                { "containsNulls", message == null || channel == null },
                { "containsBadCaches", !cachedMessage.HasValue || !cachedChannel.HasValue }
            };
        }
    }
}
