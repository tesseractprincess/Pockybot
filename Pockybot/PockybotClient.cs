using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using Pockybot.Core.Interfaces;

namespace Pockybot
{
    internal sealed class PockybotClient
    {
        private DiscordSocketClient _client;
        private readonly IEnumerable<IPockybotHandler> _handlers;
        private readonly ILogger<PockybotClient> _logger;

        public PockybotClient(DiscordSocketClient client, IEnumerable<IPockybotHandler> handlers, ILogger<PockybotClient> logger)
        {
            _client = client;
            _handlers = handlers;
            _logger = logger;
        }

        public async Task Initialize()
        {
            _client.Log += Log;

            foreach (var handler in _handlers)
                handler.Register(_client);

            await _client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("PockybotToken", EnvironmentVariableTarget.User));
            await _client.StartAsync();
        }

        private Task Log(LogMessage message)
        {
            switch (message.Severity)
            {
                case LogSeverity.Critical:
                    _logger.LogCritical(message.ToString());
                    break;
                case LogSeverity.Error:
                    _logger.LogError(message.ToString());
                    break;
                case LogSeverity.Warning:
                    _logger.LogWarning(message.ToString());
                    break;
                case LogSeverity.Info:
                    _logger.LogInformation(message.ToString());
                    break;
                case LogSeverity.Debug:
                case LogSeverity.Verbose:
                    _logger.Log(LogLevel.Debug, message.ToString());
                    break;
                default: 
                    _logger.Log(LogLevel.None, message.ToString()); 
                    break;
            }

            return Task.CompletedTask;
        }
    }
}
