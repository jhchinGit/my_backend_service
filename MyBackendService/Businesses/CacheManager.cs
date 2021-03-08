using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace MyBackendService.Businesses
{
    public class CacheManager : ICacheManager
    {
        private readonly IDistributedCache _cache;
        private readonly int EXPIRE_HOUR = 6;

        public CacheManager(IDistributedCache cache) => _cache = cache;

        public async Task<bool> SetAsync<T>(string key, T objectToBeSet, DateTime? expireDate = null)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return false;
            }

            await _cache.SetStringAsync(key, JsonConvert.SerializeObject(objectToBeSet), new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = expireDate ?? new DateTimeOffset(DateTime.Now.AddHours(EXPIRE_HOUR))
            });

            return true;
        }

        public async Task<(bool isSuccess, T result)> GetAsync<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return (false, default(T));
            }

            var isSuccess = false;
            var result = default(T);

            try
            {
                var resultJson = await _cache.GetStringAsync(key);
                result = JsonConvert.DeserializeObject<T>(resultJson);
                isSuccess = true;
            }
            catch (Exception)
            {

            }

            return (isSuccess, result);
        }
    }
}