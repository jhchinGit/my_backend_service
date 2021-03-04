using System;

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

            return Math.Round((((decimal)todayCase / (decimal)yesterdayCase) - 1) * 100, 4);
        }

        public static string ToDynamicString(this DateTime sourceDate)
        {
            return sourceDate.ToString("yyyy-MM-ddTHH:mm:ss.fffK");
        }
    }
}