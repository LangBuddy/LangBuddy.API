using Database.Data;

namespace Database.Common
{
    public interface ICacheService
    {
        //Task<T?> GetByStringAsync<T>(string key, CancellationToken cancellationToken = default)
        //    where T : class;

        //Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default)
        //    where T : class;

        //Task RemoveAsync(string key, CancellationToken cancellation = default);

        //Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellation = default);

        public Task<T> GetData<T>(string id, CachePrefixes cachePrefixes, CancellationToken cancellation = default)
            where T : class;

        public Task SetData<T>(string id, CachePrefixes cachePrefixes, T value, CancellationToken cancellation = default);

        public Task RemoveData(string id, CachePrefixes cachePrefixes, CancellationToken cancellation = default);
    }
}
