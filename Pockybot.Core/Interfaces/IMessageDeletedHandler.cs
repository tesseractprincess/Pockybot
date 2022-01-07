using Discord;

namespace Pockybot.Core.Interfaces
{
    public interface IMessageDeletedHandler : IPockybotHandler
    {
        Task Handle(Cacheable<IMessage, ulong> cachedMessage, Cacheable<IMessageChannel, ulong> cachedChannel);
    }
}
