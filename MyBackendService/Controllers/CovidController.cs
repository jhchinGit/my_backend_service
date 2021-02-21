using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBackendService.Businesses;
using MyBackendService.Models;
using MyBackendService.Models.DTOs;
using System.Threading.Tasks;

namespace MyBackendService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CovidController : ApiControllerBase
    {
        private readonly ILogger<CovidController> _logger;
        private readonly ICovidDailyReportManager _dailyReportManager;

        public CovidController(ILogger<CovidController> logger, ICovidDailyReportManager dailyReportManager)
        {
            _logger = logger;
            _dailyReportManager = dailyReportManager;
        }

        [HttpGet("{country}")]
        public async Task<ActionResult<CovidDailyReport>> Get(Country country)
        {
            CovidDailyReport result = null;
            var errorMessage = string.Empty;

            await _dailyReportManager.GetDailyReportAsync(country,
                onSuccess: (dailyReport) =>
                {
                    result = dailyReport;
                },
                onError: (errorMessage) =>
                {
                });

            if (result == null)
            {
                return BadRequest(errorMessage);
            }
            else
            {
                return Ok(GenerateResponse(data: result));
            }
        }
    }
}