using System;

namespace MyBackendService.Models.DTOs
{
    public record MalaysiaCovidReport
    {
        public int TestedPositive { get; init; }
        public int Recovered { get; init; }
        public int ActiveCase { get; init; }
        public int Deceased { get; init; }
        public DateTime LastUpdatedAtApify { get; init; }
    }
}