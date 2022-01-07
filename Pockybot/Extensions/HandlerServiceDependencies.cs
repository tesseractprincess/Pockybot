using Microsoft.Extensions.DependencyInjection;
using Pockybot.Application.Handlers;
using Pockybot.Core.Interfaces;

namespace Pockybot.Extensions
{
    internal static class HandlerServiceDependencies
    {
        public static IServiceCollection AddHandlerServices(this IServiceCollection services) =>
            services
                .AddTransient<IPockybotHandler, MessageReceivedHandler>()
                .AddTransient<IPockybotHandler, DuplicateMessageHandler>();
    }
}
