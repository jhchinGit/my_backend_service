using MyBackendService.Models.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyBackendService.Services
{
    public class IndiaCovidService
    {
        private readonly IHttpClientFactory _clientFactory;
        public bool IsSuccess { get; private set; }

        public IndiaCovidService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IndiaCovidReport> GetCovidReportAsync()
        {
            IsSuccess = false;

            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://api.apify.com/v2/key-value-stores/toDWvRj1JpTXiM8FF/records/LATEST?disableRedirect=true");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = new IndiaCovidReport();

            try
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<IndiaCovidReport>(responseJson);

                IsSuccess = result != null;
            }
            catch
            {
            }

            return result;
        }

        private static T ExtractJToken<T>(string key, JToken jToken)
        {
            if (jToken[key] == null)
            {
                return default;
            }

            return jToken[key].ToObject<T>();
        }
    }
}