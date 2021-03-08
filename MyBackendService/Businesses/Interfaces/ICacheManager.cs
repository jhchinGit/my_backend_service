using System;
using System.Threading.Tasks;

namespace MyBackendService.Businesses
{
    public interface ICacheManager
    {
        Task<bool> SetAsync<T>(string key, T objectToBeSet, DateTime? expireDate = null);

        Task<(bool isSuccess, T result)> GetAsync<T>(string key);
    }
}