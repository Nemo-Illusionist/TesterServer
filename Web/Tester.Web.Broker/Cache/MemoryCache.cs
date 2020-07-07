using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Tester.Web.Broker.Cache
{
    public class MemoryCache: ICache
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public void Dispose()
        {
        }

        public string Get(string key)
        {
            return _memoryCache.Get<string>(key);
        }

        public void Set(string key, string value)
        {
            _memoryCache.Set(key, value);
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public void Set<T>(string key, T value)
        {
            _memoryCache.Set(key, value);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public Task<T> GetAsync<T>(string key)
        {
            throw new NotImplementedException();
        }

        public Task SetAsync<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetAsync(string key)
        {
            return Task.FromResult(Get(key));
        }

        public Task SetAsync(string key, string value)
        {
            Set(key, value);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(string key)
        {
            Remove(key);
            return Task.CompletedTask;
        }
    }
}