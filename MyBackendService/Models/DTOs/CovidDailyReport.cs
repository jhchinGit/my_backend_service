using Newtonsoft.Json;
using System;

namespace MyBackendService.Models.DTOs
{
    public record CovidDailyReport
    {
        [JsonProperty("totalCase")]
        public int TotalCase { get; init; }

        [JsonProperty("newCase")]
        public int NewCase { get; init; }

        [JsonProperty("newCaseRate")]
        public decimal NewCaseRate { get; init; }

        [JsonProperty("recovered")]
        public int Recovered { get; init; }

        [JsonProperty("recoveredNew")]
        public int RecoveredNew { get; init; }

        [JsonProperty("recoveredNew")]
        public decimal RecoveredRate { get; init; }

        [JsonProperty("activeCase")]
        public int ActiveCase { get; init; }

        [JsonProperty("todayActiveCase")]
        public int TodayActiveCase { get; init; }

        [JsonProperty("activeRate")]
        public decimal ActiveRate { get; init; }

        [JsonProperty("death")]
        public int Death { get; init; }

        [JsonProperty("todayDeathCase")]
        public int TodayDeathCase { get; init; }

        [JsonProperty("deathRate")]
        public decimal DeathRate { get; init; }

        [JsonProperty("reportedDate")]
        public DateTime ReportedDate { get; init; }
    }
}