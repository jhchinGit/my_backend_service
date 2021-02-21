namespace MyBackendService.Utility
{
    public static class CovidReportHelper
    {
        public static decimal CalculateRate(int todayCase, int yesterdayCase)
        {
            if (yesterdayCase == 0)
            {
                return todayCase > 0 ? 100 : 0;
            }

            return (((decimal)todayCase / (decimal)yesterdayCase) - 1) * 100;
        }
    }
}