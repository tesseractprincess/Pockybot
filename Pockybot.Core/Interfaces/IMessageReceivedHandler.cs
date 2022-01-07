using Discord.WebSocket;

namespace Pockybot.Core.Interfaces
{
    public interface IMessageReceivedHandler : IPockybotHandler
    {
        Task Handle(SocketMessage message);
    }
}
