using Discord.WebSocket;

namespace Pockybot.Core.Interfaces
{
    public interface IPockybotHandler
    {
        void Register(DiscordSocketClient client);
    }
}
