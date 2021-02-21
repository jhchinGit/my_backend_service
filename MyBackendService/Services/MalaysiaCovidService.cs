using MyBackendService.Models;
using MyBackendService.Models.DTOs;
using Newtonsoft.Json.Linq;
using System;
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

        public async Task<RangeDto<MalaysiaCovidReport>> GetCovidReportAsync()
        {
            IsSuccess = false;

            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://api.apify.com/v2/datasets/7Fdb90FMDLZir2ROo/items?format=json&clean=1");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = new RangeDto<MalaysiaCovidReport>
            {
                Start = new MalaysiaCovidReport(),
                End = new MalaysiaCovidReport()
            };

            var responseJson = await response.Content.ReadAsStringAsync();
            try
            {
                var responseJArray = JArray.Parse(responseJson);

                if (responseJArray.Count == 0)
                {
                    return null;
                }

                var latestReportJToken = responseJArray.Last;

                result.End = new MalaysiaCovidReport
                {
                    TestedPositive = ExtractJToken<int>("testedPositive", latestReportJToken),
                    Recovered = ExtractJToken<int>("recovered", latestReportJToken),
                    ActiveCase = ExtractJToken<int>("activeCases", latestReportJToken),
                    Deceased = ExtractJToken<int>("deceased", latestReportJToken),
                    LastUpdatedAtApify = ExtractJToken<DateTime>("lastUpdatedAtApify", latestReportJToken),
                };

                for (int index = responseJArray.Count - 2; index >= 0; index--)
                {
                    var reportDate = ExtractJToken<DateTime>("lastUpdatedAtApify", responseJArray[index]);

                    if (reportDate.Year == result.End.LastUpdatedAtApify.Year &&
                        reportDate.Month == result.End.LastUpdatedAtApify.Month &&
                        reportDate.Day == result.End.LastUpdatedAtApify.Day)
                    {
                        continue;
                    }
                    else
                    {
                        result.Start = new MalaysiaCovidReport
                        {
                            TestedPositive = ExtractJToken<int>("testedPositive", responseJArray[index]),
                            Recovered = ExtractJToken<int>("recovered", responseJArray[index]),
                            ActiveCase = ExtractJToken<int>("activeCases", responseJArray[index]),
                            Deceased = ExtractJToken<int>("deceased", responseJArray[index]),
                            LastUpdatedAtApify = ExtractJToken<DateTime>("lastUpdatedAtApify", responseJArray[index]),
                        };
                        break;
                    }
                }

                IsSuccess = true;
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