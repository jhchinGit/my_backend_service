using MyBackendService.Models;
using MyBackendService.Models.DTOs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyBackendService.Services
{
    public class MalaysiaCovidService
    {
        private readonly IHttpClientFactory _clientFactory;
        public bool IsSuccess { get; private set; }

        public MalaysiaCovidService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<RangeDto<MalaysiaCovidReport>> GetMalaysiaCovidReportAsync()
        {
            IsSuccess = false;

            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://api.apify.com/v2/datasets/7Fdb90FMDLZir2ROo/items?format=json&clean=1");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                var responseJArray = JArray.Parse(responseJson);
                var lastJArray = responseJArray.Last;
                var lastJarray2 = responseJArray[responseJArray.Count - 1];
                return null;
            }
            else
            {
                return null;
            }
        }
    }
}
