using Discord.WebSocket;
using Pockybot.Core.Entities;

namespace Pockybot.Application.Composers
{
    public sealed class UserMessageComposer
    {
        public UserMessageEntity DeserializeFromMessage(SocketMessage message, SocketGuildChannel channel)
        {
            return new UserMessageEntity
            {
                EntityId = message.Id,
                OwnerId = message.Author.Id,
                ChannelId = channel.Id,
                GuildId = channel.Guild.Id,
                Content = message.Content
            };
        }
    }
}
