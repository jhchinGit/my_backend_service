﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBackendService.Businesses;
using MyBackendService.Models;
using MyBackendService.Models.DTOs;
using System.Threading.Tasks;

namespace MyBackendService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CovidController : ApiControllerBase
    {
        private readonly ICovidDailyReportManager _dailyReportManager;

        public CovidController(ICovidDailyReportManager dailyReportManager) => _dailyReportManager = dailyReportManager;

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