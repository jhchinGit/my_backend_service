using MyBackendService.Utility;
using Shouldly;
using Xunit;

namespace MyBackendService_Tests
{
    public class CommonMethod_Test
    {
        [Theory]
        [InlineData(2, 1, 100d)]
        [InlineData(-1, 1, -200d)]
        [InlineData(0, 1, -100d)]
        [InlineData(1, 1, 0d)]
        [InlineData(1, 0, 100d)]
        [InlineData(25, 20, 25d)]
        public void GIVEN_todayCase_yesterdayCase___WHEN_CalculateRate___RETURN_Percentange(
            int todayCase, int yesterdayCase, decimal expectedResult)
        {
            var actualResult = CovidReportHelper.CalculateRate(todayCase, yesterdayCase);

            expectedResult.ShouldBe(actualResult);
        }
    }
}