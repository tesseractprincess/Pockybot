using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using Pockybot.Core.Interfaces;

namespace Pockybot.Application.Handlers
{
    public sealed class MessageReceivedHandler : IPockybotHandler
    {
        private readonly ILogger _logger;

        public MessageReceivedHandler(ILogger<MessageReceivedHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(SocketMessage message)
        {
            SocketGuildChannel? channel = message.Channel as SocketGuildChannel;

            if (channel == null || message == null) throw new NullReferenceException("MessageReceivedHandler could not convert SocketMessage.Channel to SocketGuildChannel.");

            _logger.LogInformation($"[{channel?.Guild.Name}]/[{channel?.Name}]/[{message.Author.Username}]: {message.Content}");
            return Task.CompletedTask;
        }

        public void Register(DiscordSocketClient client)
        {
            client.MessageReceived += Handle;
        }
    }
}
