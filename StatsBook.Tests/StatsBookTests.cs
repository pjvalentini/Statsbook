using System;
using Xunit;

namespace StatsBook.Tests
{
    // You have to add a reference to the StatsBook project in order to have access to the StatisticsData class
    // Rt. click on the StatsBook.Tests Project and select the StatsBook.csproj
    // The error message will change from No assmebly reference to inaccesible due to protection level.
    public class StatsBookTests
    {
        [Fact]
        public void StatsBookCalculatesTheAverageGoalsPerYear()
        {
            // Arrange 
            var statisticsData = new InMemoryStatisticsData("Ligue 1 2019 Statistics"); // The SD class will be "inaccessible" until you make the SD class public.
            statisticsData.AddGoalsFromMatch(1.0);
            statisticsData.AddGoalsFromMatch(0.0);
            statisticsData.AddGoalsFromMatch(3.0);

            // Act
            var result = statisticsData.GetGoalScoringStatistics();

            // Assert 
            // Equal can take a 3rd precison paramater. Helps with rounding repeating numbers.
            Assert.Equal(1.3, result.Average, 1);
            Assert.Equal(3.0, result.Highest, 1);
            Assert.Equal(0.0, result.Lowest, 1);
            Assert.Equal('B', result.Letter);
        }

        [Fact] // ?
        public void AddGradeLogicWorks()
        {
            // Arrange 
            // This test will determine if the logic inside of AddGoalsFromMatch method is working.
            var statisticsData = new InMemoryStatisticsData("Ligue 1 2019 Statistics"); 
            statisticsData.AddGoalsFromMatch(10.0);

            // Act
            var result = statisticsData.GetGoalScoringStatistics();

            // Assert 
            Assert.Equal(10, result.Average);
        }

    }
}
