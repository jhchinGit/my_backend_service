using MyBackendService.Models;
using MyBackendService.Models.DTOs;
using MyBackendService.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyBackendService.Businesses
{
    public class CovidDailyReportManager : ICovidDailyReportManager
    {
        private readonly IHttpClientFactory _clientFactory;

        public CovidDailyReportManager(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task GetDailyReportAsync(Country country,
            Action<CovidDailyReport> onSuccess, Action<string> onError)
        {
            switch (country)
            {
                case Country.Malaysia:
                    var (isMalaysiaSuccess, malaysiaCovidDailyReport) = await GetMalaysiaReportAsync();
                    if (isMalaysiaSuccess)
                    {
                        onSuccess(malaysiaCovidDailyReport);
                    }
                    else
                    {
                        onError($"Fail to retrieve Malaysia report.");
                    }
                    break;

                case Country.India:
                    var (isIndiaSuccess, indiaCovidDailyReport) = await GetIndiaReportAsync();
                    if (isIndiaSuccess)
                    {
                        onSuccess(indiaCovidDailyReport);
                    }
                    else
                    {
                        onError($"Fail to retrieve India report.");
                    }
                    break;

                default:
                    onError($"Report is not available in {country}.");
                    break;
            }
        }

        private async Task<(bool isSuccess, CovidDailyReport dailyReport)> GetMalaysiaReportAsync()
        {
            var service = new MalaysiaCovidService(_clientFactory);
            var malaysiaCovidReportsRange = await service.GetCovidReportAsync();

            if (!service.IsSuccess ||
                (malaysiaCovidReportsRange.Start == null && malaysiaCovidReportsRange.End == null))
            {
                return (false, null);
            }

            if (malaysiaCovidReportsRange.Start == null)
            {
                return (true, new CovidDailyReport
                {
                    TotalCase = malaysiaCovidReportsRange.End.TestedPositive,
                    NewCase = malaysiaCovidReportsRange.End.TestedPositive,
                    NewCaseRate = CovidReportHelper.CalculateRate(
                        todayCase: malaysiaCovidReportsRange.End.TestedPositive, yesterdayCase: 0),

                    Recovered = malaysiaCovidReportsRange.End.Recovered,
                    RecoveredNew = malaysiaCovidReportsRange.End.Recovered,
                    RecoveredRate = CovidReportHelper.CalculateRate
                    (todayCase: malaysiaCovidReportsRange.End.Recovered, yesterdayCase: 0),

                    ActiveCase = malaysiaCovidReportsRange.End.ActiveCase,
                    TodayActiveCase = malaysiaCovidReportsRange.End.ActiveCase,
                    ActiveRate = CovidReportHelper.CalculateRate(
                        todayCase: malaysiaCovidReportsRange.End.ActiveCase, yesterdayCase: 0),

                    Death = malaysiaCovidReportsRange.End.Deceased,
                    TodayDeathCase = malaysiaCovidReportsRange.End.Deceased,
                    DeathRate = CovidReportHelper.CalculateRate(
                        todayCase: malaysiaCovidReportsRange.End.Deceased, yesterdayCase: 0),

                    ReportedDate = malaysiaCovidReportsRange.End.LastUpdatedAtApify
                });
            }
            else
            {
                return (true, new CovidDailyReport
                {
                    TotalCase = malaysiaCovidReportsRange.End.TestedPositive,
                    NewCase = malaysiaCovidReportsRange.End.TestedPositive -
                    malaysiaCovidReportsRange.Start.TestedPositive,
                    NewCaseRate = CovidReportHelper.CalculateRate
                    (todayCase: malaysiaCovidReportsRange.End.TestedPositive,
                    yesterdayCase: malaysiaCovidReportsRange.Start.TestedPositive),

                    Recovered = malaysiaCovidReportsRange.End.Recovered,
                    RecoveredNew = malaysiaCovidReportsRange.End.Recovered -
                    malaysiaCovidReportsRange.Start.Recovered,
                    RecoveredRate = CovidReportHelper.CalculateRate
                    (todayCase: malaysiaCovidReportsRange.End.Recovered,
                    yesterdayCase: malaysiaCovidReportsRange.Start.Recovered),

                    ActiveCase = malaysiaCovidReportsRange.End.ActiveCase,
                    TodayActiveCase = malaysiaCovidReportsRange.End.ActiveCase -
                    malaysiaCovidReportsRange.Start.ActiveCase,
                    ActiveRate = CovidReportHelper.CalculateRate
                    (todayCase: malaysiaCovidReportsRange.End.ActiveCase,
                    yesterdayCase: malaysiaCovidReportsRange.Start.ActiveCase),

                    Death = malaysiaCovidReportsRange.End.Deceased,
                    TodayDeathCase = malaysiaCovidReportsRange.End.Deceased -
                     malaysiaCovidReportsRange.Start.Deceased,
                    DeathRate = CovidReportHelper.CalculateRate
                    (todayCase: malaysiaCovidReportsRange.End.Deceased,
                    yesterdayCase: malaysiaCovidReportsRange.Start.Deceased),

                    ReportedDate = malaysiaCovidReportsRange.End.LastUpdatedAtApify.AddHours(4)
                });
            }
        }

        private async Task<(bool isSuccess, CovidDailyReport dailyReport)> GetIndiaReportAsync()
        {
            var service = new IndiaCovidService(_clientFactory);
            var indiaCovidReport = await service.GetCovidReportAsync();

            if (!service.IsSuccess || indiaCovidReport == null)
            {
                return (false, null);
            }

            var newCase = indiaCovidReport.ActiveCasesNew +
                indiaCovidReport.RecoveredNew +
                indiaCovidReport.DeathsNew;
            var yesterdayTotalCase = indiaCovidReport.TotalCases - newCase;
            var yesterdayNewCase = indiaCovidReport.ActiveCases - indiaCovidReport.ActiveCasesNew;
            var yesterdayRecoveredCase = indiaCovidReport.Recovered - indiaCovidReport.RecoveredNew;
            var yesterdayDeathCase = indiaCovidReport.Deaths - indiaCovidReport.DeathsNew;

            return (true, new CovidDailyReport
            {
                TotalCase = indiaCovidReport.TotalCases,
                NewCase = newCase,
                NewCaseRate = CovidReportHelper.CalculateRate
                     (todayCase: indiaCovidReport.TotalCases,
                     yesterdayCase: yesterdayTotalCase),

                Recovered = indiaCovidReport.Recovered,
                RecoveredNew = indiaCovidReport.RecoveredNew,
                RecoveredRate = CovidReportHelper.CalculateRate
                     (todayCase: indiaCovidReport.Recovered,
                     yesterdayCase: yesterdayRecoveredCase),

                ActiveCase = indiaCovidReport.ActiveCases,
                TodayActiveCase = indiaCovidReport.ActiveCasesNew,
                ActiveRate = CovidReportHelper.CalculateRate
                     (todayCase: indiaCovidReport.ActiveCases,
                     yesterdayCase: yesterdayNewCase),

                Death = indiaCovidReport.Deaths,
                TodayDeathCase = indiaCovidReport.DeathsNew,
                DeathRate = CovidReportHelper.CalculateRate
                     (todayCase: indiaCovidReport.Deaths,
                     yesterdayCase: yesterdayDeathCase),

                ReportedDate = indiaCovidReport.LastUpdatedAtApify.AddHours(4)
            });
        }
    }
}