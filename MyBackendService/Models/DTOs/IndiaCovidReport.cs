using System;

namespace MyBackendService.Models.DTOs
{
    public record IndiaCovidReport
    {
        public int ActiveCases { get; init; }
        public int ActiveCasesNew { get; init; }
        public int Recovered { get; init; }
        public int RecoveredNew { get; init; }
        public int Deaths { get; init; }
        public int DeathsNew { get; init; }
        public int TotalCases { get; init; }
        public DateTime LastUpdatedAtApify { get; init; }
    }
}