using Database.Common;
using Database.Data;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Database
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;

        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T?> GetData<T>(string id, CachePrefixes cachePrefixes, CancellationToken cancellation = default) where T : class
        {
            string key = $"{cachePrefixes}/{id}";

            var value = await _distributedCache.GetStringAsync(key, cancellation);

            if(!string.IsNullOrEmpty(value))
            {
                return JsonConvert.DeserializeObject<T>(value);
            }

            return null;
        }

        public async Task RemoveData(string id, CachePrefixes cachePrefixes, CancellationToken cancellation = default)
        {
            string key = $"{cachePrefixes}/{id}";

            await _distributedCache.RemoveAsync(key, cancellation);
        }

        public async Task SetData<T>(string id, CachePrefixes cachePrefixes, T value, CancellationToken cancellation = default)
        {
            string key = $"{cachePrefixes}/{id}";

            string cacheValue = JsonConvert.SerializeObject(value);
            await _distributedCache.SetStringAsync(key, cacheValue, cancellation);
        }

        //public async Task<T?> GetByStringAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
        //{
        //    var cacheValue = await _distributedCache.GetStringAsync(key, cancellationToken);

        //    if (cacheValue == null)
        //    {
        //        return null;
        //    }

        //    T? value = JsonConvert.DeserializeObject<T>(cacheValue);

        //    return value;
        //}

        //public async Task<T?> GetAsync<T>(string key, Func<Task<T>> factory, CancellationToken cancellationToken = default) where T : class
        //{
        //    var cachedValue = await GetByStringAsync<T>(key, cancellationToken);

        //    if (cachedValue is not null)
        //    {
        //        return cachedValue;
        //    }

        //    cachedValue = await factory();

        //    await SetAsync(key, cachedValue, cancellationToken);

        //    return cachedValue;
        //}

        //public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default) where T : class
        //{
        //    string cacheValue = JsonConvert.SerializeObject(value);

        //    await _distributedCache.SetStringAsync(key, cacheValue, cancellationToken);
        //}

        //public async Task RemoveAsync(string key, CancellationToken cancellation = default)
        //{
        //    await _distributedCache.RemoveAsync(key, cancellation);
        //}

        //public async Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellation = default)
        //{
        //    var tasks = _cacheKeys.Keys
        //        .Where(key => key.StartsWith(prefixKey))
        //        .Select(key => RemoveAsync(key, cancellation));

        //    await Task.WhenAll(tasks);
        //}
    }
}
