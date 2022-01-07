using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using Pockybot.Application.Composers;
using Pockybot.Application.Helpers;
using Pockybot.Core.Entities;
using Pockybot.Core.Interfaces;
using System.Text;

namespace Pockybot.Application.Handlers
{
    public sealed class DuplicateMessageHandler : IMessageReceivedHandler, IMessageDeletedHandler, IMessageUpdatedHandler
    {
        private readonly ILogger<DuplicateMessageHandler> _logger;
        private readonly ICacheService<UserMessageEntity> _messageCache;
        private readonly UserMessageComposer _composer;

        public DuplicateMessageHandler(ILogger<DuplicateMessageHandler> logger, ICacheService<UserMessageEntity> messageCache, UserMessageComposer composer)
        {
            _logger = logger;
            _messageCache = messageCache;
            _composer = composer;
        }

        public void Register(DiscordSocketClient client)
        {
            client.MessageReceived += Handle;
            client.MessageDeleted += Handle;
            client.MessageUpdated += Handle;
        }

        public Task Handle(SocketMessage message)
        {
            SocketGuildChannel? channel = message.Channel as SocketGuildChannel;
            if (channel == null || message == null) throw new NullReferenceException("MessageReceivedHandler could not convert SocketMessage.Channel to SocketGuildChannel.");

            var entity = _composer.DeserializeFromMessage(message, channel);
            entity.ExpiresAt = message.Timestamp.AddMinutes(10).ToUnixTimeMilliseconds();

            if (!ValidateDuplicateEntity(entity))
                _messageCache.AddEntity(_composer.DeserializeFromMessage(message, channel));
            else message.DeleteAsync(new RequestOptions { AuditLogReason = "Similar message exists." });

            return Task.CompletedTask;
        }

        public Task Handle(Cacheable<IMessage, ulong> cachedMessage, Cacheable<IMessageChannel, ulong> cachedChannel)
        {
            if (_messageCache.HasEntity(cachedMessage.Id))
                _messageCache.RemoveEntity(cachedMessage.Id);

            return Task.CompletedTask;
        }

        public Task Handle(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
        {
            return Task.CompletedTask;
        }

        private bool ValidateDuplicateEntity(UserMessageEntity entity)
        {
            bool thresholdTriggered = false;
            StringBuilder sb = new();
            sb.AppendLine($"Similarity to: {entity.Content}...");

            foreach (UserMessageEntity message in _messageCache.Entities.Where(e => e.OwnerId == entity.OwnerId))
            {
                double distance = LevenshteinDistance.Calculate(entity.Content.ToLower() ?? "", message.Content.ToLower() ?? "") / (double)Math.Max(entity.Content.Length, message.Content.Length);

                sb.AppendLine($"Cached Entity: {message.Content}");
                sb.AppendLine($"Score: {distance}\n");
                if (distance < 0.3)
                {
                    thresholdTriggered = true;
                    break;
                }
            }

            _logger.LogInformation(sb.ToString());
            return thresholdTriggered;
        }
    }
}
