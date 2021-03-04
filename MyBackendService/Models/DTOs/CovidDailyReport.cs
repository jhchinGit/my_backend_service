namespace MyBackendService.Models.DTOs
{
    public record CovidDailyReport
    {
        public int TotalCase { get; init; }
        public int NewCase { get; init; }
        public decimal NewCaseRate { get; init; }
        public int Recovered { get; init; }
        public int RecoveredNew { get; init; }
        public decimal RecoveredRate { get; init; }
        public int ActiveCase { get; init; }
        public int TodayActiveCase { get; init; }
        public decimal ActiveRate { get; init; }
        public int Death { get; init; }
        public int TodayDeathCase { get; init; }
        public decimal DeathRate { get; init; }
        public string ReportedDateStr { get; init; }
    }
}