using System;
using System.Collections.Generic;

namespace StatsBook
{
    class Program
    {
        // Static refers to NOT being associated to the object instance, but rather to the type that they are defined inside of.
        // Statics negate the benefits of OOP.

        static void Main(string[] args)
        {
            IStatisticsData statisticsData = new StatisticsDataToDisk("Ligue 1 2019 Statistics");
            statisticsData.GoalStatAdded += OnGoalStatAdded;

            EnterGoalStats(statisticsData);


            // holds the returned value of GetGoalScoringStatistics()
            var stats = statisticsData.GetGoalScoringStatistics();

            Console.WriteLine($"Sports Category: {InMemoryStatisticsData.CATEGORY}");
            Console.WriteLine($"{statisticsData.Name}");
            Console.WriteLine($"The Player Played {stats.TotalGamesPlayed} regular season games.");
            Console.WriteLine($"The Player's total season goals: {stats.TotalGoalsScored} goals");
            Console.WriteLine($"The Player's Average Goals Per Game = {stats.Average:N1} GPG");
            Console.WriteLine($"Best Performance in a match: {stats.Highest:N0} goals");
            Console.WriteLine($"Worst Performance in a match: {stats.Lowest:N0} goals");
            Console.WriteLine($"The Average Player Rating is: Grade {stats.Letter}");
        }

        // here we want parameter to change the base class as we dont know which type will derive from the base class.
        // we know its ploymorphic based on the type of object we are working with at that given time.
        private static void EnterGoalStats(IStatisticsData statisticsData)
        {
            while (true)
            {
                Console.WriteLine("Enter a Goal Stat or Enter a 'q' to quit.");
                var input = Console.ReadLine();

                if (input == "q")
                {
                    break;
                }

                try
                {
                    var goalStat = double.Parse(input);
                    statisticsData.AddGoalsFromMatch(goalStat);
                }
                // These two errors I know can occur here.  Write try catch blocks for errors you know you need to handle.
                // Catches if value is out of the range in AddGoalsFromMatch()
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                // Catches if value is not in the correct format AddGoalsFromMatch()
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.WriteLine("**");
                }
            }
        }

        static void OnGoalStatAdded(object sender, EventArgs e)
        {
            Console.WriteLine("Goal Stat Added!");
        }
    }
}
