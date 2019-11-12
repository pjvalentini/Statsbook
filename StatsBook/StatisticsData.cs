using System;
using System.Collections.Generic;
using System.IO;

// I want to consider the abstractions i will need for this class, consider the method and members that will live here.
// The State and The Behavior are the core of a Class.
// This class will hold the scores and perform actions on that data.
namespace StatsBook
{
    public delegate void GoalStatAddedDelegate(object sender, EventArgs args);

    // An interface is pure, it contains no implmentation details
    // We have to define abstractly the members that will be available for implementation
    // More common than abstract classes.
    // We have an interface that defines the capability of any Statistics data where I want to store and compute stats
    public interface IStatisticsData
    {
        void AddGoalsFromMatch(double singleGoalMatchStat);
        Statistics GetGoalScoringStatistics();
        string Name { get; }
        event GoalStatAddedDelegate GoalStatAdded;
    }


    // This is a base class for other types of StatisticsData
    // Using plymorphism so that classes that derive from StatisticsData_base can have different implemntations of AddGoalsFromMatch().
    // Any Class of StatisticsData_Base should have an AddGoalsToMatch method that takes a double.
    // We can add an aditional interface, not an inherited class. I can specify the interfaces I want to implement.
    // We need to implement the interface since I need to have all of its members available to it.
    public abstract class StatisticsData_Base : NamedObject, IStatisticsData
    {
        public StatisticsData_Base(string name) : base(name)
        {

        }

        // The virtual keyword is used to modify a method, property, indexer, or event declared in the base class and allow it to be overridden in the derived class. 
        public abstract event GoalStatAddedDelegate GoalStatAdded;

        // can provide a class member but not an implementation with an abtract method.
        // Let the derived types that inherit from this base class determine the implementation.
        public abstract void AddGoalsFromMatch(double singlematchGoalStat);

        public abstract Statistics GetGoalScoringStatistics();
       
    }

    // Here we create a new class that inherits and implements the members of the Base class.
    public class StatisticsDataToDisk : StatisticsData_Base
    {
        public StatisticsDataToDisk(string name) : base(name)
        {
        }

        public override event GoalStatAddedDelegate GoalStatAdded;

        public override void AddGoalsFromMatch(double singlematchGoalStat)
        {
            // This pattern can be used with IDisposables, create then dispose the object.
            // Whatever is in the curlys with have access to the Dispose() method
            using (var writeToFile = File.AppendText($"{Name}.txt"))
            {
                writeToFile.WriteLine(singlematchGoalStat);
                // we can raise the event here after we write the stat
                if (GoalStatAdded != null)
                {
                    GoalStatAdded(this, new EventArgs());
                }
            }
        }

        public override Statistics GetGoalScoringStatistics()
        {
            var result = new Statistics();

            using(var reader = File.OpenText($"{Name}.txt"))
            {
                var line = reader.ReadLine();
                while(line != null)
                {
                    var number = double.Parse(line); // Getting input string wrong format becasue im exiting with q...Need to figure put why.
                    result.Add(number);
                    line = reader.ReadLine();
                }
            }

            return result;
        }
    }

    // Using inheritance to inherit the NamedObject  
    // We can add an aditional interface, not an inherited class. I can specify the interfaces I want to implement.
    public class InMemoryStatisticsData : StatisticsData_Base
    {
        private List<double> _gamesPlayed;
        public const string CATEGORY = "French 1st Division Football";

        // ctor
        // base class requires a name parameter that is a string.
        // base accesses the NamedObject ctor.
        public InMemoryStatisticsData(string name) : base(name)
        {
            // new up the List
            _gamesPlayed = new List<double>();
            Name = name;
        }

        // Map a char to number
       

        // instance method of the class StatisticsData
        // here we override what ever the base class is providing, polymorphism allows us to override the method of a base class.
        public override void AddGoalsFromMatch(double singleMatchGoalStat)
        {
            // setting a range between 0 and 100 for a singleMatchGoalStat.
            if (singleMatchGoalStat <= 100 && singleMatchGoalStat >= 0)
            {
                _gamesPlayed.Add(singleMatchGoalStat);
                // this is the sender, and the new instance of the EventArgs class is needed as well based on the signature.
                if (GoalStatAdded != null)
                {
                    GoalStatAdded(this, new EventArgs());
                }

            }
            else
            {
                // we throw an exception here because we want to catch bad data.  We only want a double.
                throw new ArgumentException($"Invalid {nameof(singleMatchGoalStat)}"); 
            }
        }

        // An event can be a member of a class
        // This is just a field on the StatisticsData class.
        // Every StatisticsData object will have a GoalStatAdded event.
        //  The override keyword is used to extend or modify a virtual/abstract method, property, indexer, or event of base class into derived class
        public override event GoalStatAddedDelegate GoalStatAdded;

        // Method on the Statistics object. Returns an object of type Statistics.
        public override Statistics GetGoalScoringStatistics()
        {
            // to return an obj of type statistics I need to set up an new obj.
            // result references the type Statistics.
            var result = new Statistics();

            for (var i = 0; i < _gamesPlayed.Count; i += 1)
            {
                result.Add(_gamesPlayed[i]);
            }

            return result;
        }
    }
}
