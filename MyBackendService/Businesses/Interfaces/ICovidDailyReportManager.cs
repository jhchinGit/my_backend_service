using MyBackendService.Models;
using MyBackendService.Models.DTOs;
using System;
using System.Threading.Tasks;

namespace MyBackendService.Businesses
{
    public interface ICovidDailyReportManager
    {
        Task GetDailyReportAsync(Country country, Action<CovidDailyReport> onSuccess, Action<string> onError);
    }
}