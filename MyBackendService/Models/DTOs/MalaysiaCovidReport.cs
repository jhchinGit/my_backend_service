using System;

namespace MyBackendService.Models.DTOs
{
    public class MalaysiaCovidReport
    {
        public int TestedPositive { get; set; }
        public int Recovered { get; set; }
        public int ActiveCases { get; set; }
        public int Deceased { get; set; }
        public DateTime LastUpdatedAtApify { get; set; }
    }
}