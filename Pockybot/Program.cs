using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pockybot;
using Pockybot.Application.Exceptions;
using Pockybot.Extensions;

using IHost host = Host.CreateDefaultBuilder()
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    })
    .ConfigureServices((_, services) =>
        services
            .AddApplicationServices()
            .AddSingleton<DiscordSocketClient, DiscordSocketClient>(impl =>
            {
                return new DiscordSocketClient(new DiscordSocketConfig
                {
                    GatewayIntents = GatewayIntents.Guilds | GatewayIntents.GuildBans | GatewayIntents.GuildMessages | GatewayIntents.GuildMessageReactions | GatewayIntents.DirectMessages | GatewayIntents.DirectMessageReactions, 
                    MessageCacheSize = 200
                });
            })
            .AddComposerServices()
            .AddHandlerServices()
            .AddWorkerServices()
            .AddSingleton<PockybotClient, PockybotClient>())
    .Build();

AppDomain.CurrentDomain.UnhandledException += host.Services.GetRequiredService<UnhandledExceptionService>().HandleException;
await host.Services.GetRequiredService<PockybotClient>().Initialize();

await host.RunAsync();

