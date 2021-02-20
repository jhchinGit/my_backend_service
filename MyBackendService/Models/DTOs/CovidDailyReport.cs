using Newtonsoft.Json;
using System;

namespace MyBackendService.Models.DTOs
{
    public class CovidDailyReport
    {
        [JsonProperty("totalCase")]
        public int TotalCase { get; set; }

        [JsonProperty("newCase")]
        public int NewCase { get; set; }

        [JsonProperty("newCaseRate")]
        public decimal NewCaseRate { get; set; }

        [JsonProperty("recovered")]
        public int Recovered { get; set; }

        [JsonProperty("recoveredNew")]
        public int RecoveredNew { get; set; }

        [JsonProperty("recoveredNew")]
        public decimal RecoveredRate { get; set; }

        [JsonProperty("activeCase")]
        public int ActiveCase { get; set; }

        [JsonProperty("todayActiveCase")]
        public int TodayActiveCase { get; set; }

        [JsonProperty("activeRate")]
        public decimal ActiveRate { get; set; }

        [JsonProperty("death")]
        public int Death { get; set; }

        [JsonProperty("todayDeathCase")]
        public int TodayDeathCase { get; set; }

        [JsonProperty("deathRate")]
        public decimal DeathRate { get; set; }

        [JsonProperty("reportedDate")]
        public DateTime ReportedDate { get; set; }
    }
}