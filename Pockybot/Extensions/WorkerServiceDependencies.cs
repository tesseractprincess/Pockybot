using Microsoft.Extensions.DependencyInjection;
using Pockybot.Application.Workers;
using Pockybot.Core.Entities;
using Pockybot.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pockybot.Extensions
{
    internal static class WorkerServiceDependencies
    {
        public static IServiceCollection AddWorkerServices(this IServiceCollection services) =>
            services
                .AddHostedService<CacheExpiryWorker<ICacheService<UserMessageEntity>, UserMessageEntity>>();
    }
}
