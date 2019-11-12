using System;
using System.Collections.Generic;
using System.Linq;

namespace StatsBook
{
   // Here we set up an object by creating a class definition.
   public class Statistics
    {
        public int TotalGamesPlayed;
        public double TotalGoalsScored;   
        
        // add a property here so we can compute the average from inside this class.
        // we can read the average not set it here...only a getter.
        public double Average 
        {
            get
            {
                return Sum / Count;
            }
        }
        public double Highest;
        public double Lowest;
        public char Letter 
        {
            get
            {
                switch (Average)
                {
                    case var d when d >= 1.8:
                        return 'A';   

                    case var d when d >= 1.2:
                        return 'B';

                    case var d when d >= 0.5:
                        return 'C';

                    case var d when d >= 0.3:
                       return 'D';

                    default:
                       return 'F';          
                }
            }
        }

        // was null before I instantiated the list 
        public List<double> _gamesPlayed = new List<double>();
        public double Sum;
        public int Count;

        // This add method will add a number into Statistics
        public void Add(double number)
        {
            Sum += number;
            Count += 1;
            Highest = Math.Max(number, Highest);
            Lowest = Math.Min(number, Lowest);
        }
        public Statistics()
        {
            Count = 0;
            TotalGamesPlayed = _gamesPlayed.Count;
            TotalGoalsScored = _gamesPlayed.Sum();
            Sum = 0.0;
            Highest = double.MinValue;
            Lowest = double.MaxValue;
        }
    }
}
