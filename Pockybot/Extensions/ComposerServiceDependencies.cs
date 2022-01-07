using Microsoft.Extensions.DependencyInjection;
using Pockybot.Application.Composers;

namespace Pockybot.Extensions
{
    internal static class ComposerServiceDependencies
    {
        public static IServiceCollection AddComposerServices(this IServiceCollection services) =>
            services
                .AddTransient<UserMessageComposer>();
    }
}
