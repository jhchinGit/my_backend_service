using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBackendService.Models;
using MyBackendService.Services;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyBackendService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CovidController : ControllerBase
    {
        private readonly ILogger<CovidController> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public CovidController(ILogger<CovidController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        [HttpGet("{country}")]
        public async Task<ActionResult<int>> Get(Country country)
        {
            var service = new MalaysiaCovidService(_clientFactory);
            var result = await service.GetMalaysiaCovidReportAsync();
            return Ok(1);
        }
    }
}