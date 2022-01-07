using Discord;
using Discord.WebSocket;

namespace Pockybot.Core.Interfaces
{
    public interface IMessageUpdatedHandler : IPockybotHandler
    {
        Task Handle(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel);
    }
}
