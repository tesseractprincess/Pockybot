using Microsoft.Extensions.DependencyInjection;
using Pockybot.Application.Ephemeral;
using Pockybot.Application.Exceptions;
using Pockybot.Core.Entities;
using Pockybot.Core.Interfaces;

namespace Pockybot.Extensions
{
    internal static class ApplicationServiceDependencies
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
            services
                .AddSingleton<UnhandledExceptionService>()
                .AddSingleton<ICacheService<UserMessageEntity>, UserMessageCache>();
    }
}
