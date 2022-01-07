using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pockybot.Core.Interfaces;

namespace Pockybot.Application.Workers
{
    public sealed class CacheExpiryWorker<T1, T2> : BackgroundService where T1 : ICacheService<T2> where T2 : ICacheableEntity
    {
        private const int CycleWaitTime = 10000;
        private readonly ILogger<CacheExpiryWorker<T1, T2>> _logger;
        private readonly T1 _cache;

        public CacheExpiryWorker(ILogger<CacheExpiryWorker<T1, T2>> logger, T1 cache)
        {
            _logger = logger;
            _cache = cache;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                if (_cache.Entities.Any())
                {
                    var preExpiryCount = _cache.Entities.Count();
                    var result = _cache.RemoveExpiredEntities();
                    _logger.LogInformation($"Cache Expiry: Removed {result} of {preExpiryCount} {(preExpiryCount == 1 ? "entity" : "entities")}");
                }

                await Task.Delay(CycleWaitTime, stoppingToken);
            }
        }
    }
}
