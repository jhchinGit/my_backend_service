using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyBackendService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CovidController : ControllerBase
    {
        private readonly ILogger<CovidController> _logger;

        public CovidController(ILogger<CovidController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<int> Get()
        {
            return Ok(1);
        }
    }
}