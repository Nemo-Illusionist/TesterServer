using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Tester.Web.Broker.Cache
{
    public class MemoryCache : ICache
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public void Dispose()
        {
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public Task<T> GetAsync<T>(string key)
        {
            return Task.FromResult(Get<T>(key));
        }

        public void Set<T>(string key, T value)
        {
            _memoryCache.Set(key, value);
        }


        public Task SetAsync<T>(string key, T value)
        {
            Set(key, value);
            return Task.CompletedTask;
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public Task RemoveAsync(string key)
        {
            Remove(key);
            return Task.CompletedTask;
        }
    }
}