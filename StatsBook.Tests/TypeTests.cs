using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace StatsBook.Tests
{
    // I am defining a type here through the delegate, this type can be used to define variables and fields.
    // Delegates allow you to use a variable to point to a method.
    // Delgates can me multi-cast and invoke multiple methods with the same signature and return type
    public delegate string WriteLogDelegate(string logMessage);
    public class TypeTests
    {
        // this variable is used to determine how many times the method delegate is being called. 
        int count = 0;
        [Fact]
        public void WriteLogDelegateCanPointToAMethod()
        {
            // this log variable can now point to multiple methods...
            WriteLogDelegate log = ReturnMessage;

            // longform - we want to initialize log as type WriteLogDelegate and reference ReturnMessage Method
            // log = new WriteLogDelegate(ReturnMessage);
            // or
            // I can now point this log var to any method that returns a string and takes a string as parameter.
            log += ReturnMessage;
            log += IncrementCount;
            var result = log("I am logging now!");
            // Assert.Equal("I am logging now!", result);
            Assert.Equal(3, count);
        }

        string IncrementCount(string message)
        {
            count++;
            return message.ToUpper();
        }

        string ReturnMessage(string message)
        {
            count++;
            return message;
        }


        //  value type
        [Fact]
        public void ValueTypesAlsoPassByValue()
        {
            var x = GetInt();
            SetInt(x);

            Assert.Equal(3, x); 
           // Assert.Equal(42, x); // Passes if you add the ref keyword to x param in SetInt, Fails otherwise.
        }

        // 3 gets passed into the parameter x, so 42 is not passed into SetInt above.
        private void SetInt(int x)
        {
            x = 42;
        }

        private int GetInt()
        {
            return 3;
        }

        [Fact]
        public void StringsBehaveLikeValueTypes()
        {
            string name = "pj";
            var nameToUpper = MakeUppercase(name);

           // Assert.Equal("pj", name); // fails
            Assert.Equal("PJ", nameToUpper); // Passes
        }

        // Method needs to be of type string in order to return a string. 
        private string MakeUppercase(string name)
        {
          return name.ToUpper();
        }

        [Fact]
        public void CanPassParameterByReferencee()
        {
            var statsBook1 = GetStatsBook("SB 1");
            GetStatsBookSetName(ref statsBook1, "New Name");

            // Assert.Equal("SB 1", statsBook1.Name); // Fails
            Assert.Equal("New Name", statsBook1.Name); // Passes
        }

        // ref keyword says that when the method GetStatsbookSetName is called when the Parameter statsBook arrives, the first param will be passed by reference.
        // you can use out instead of ref, but out will assume that the object is initialized.
        private void GetStatsBookSetName(ref InMemoryStatisticsData statsBook, string name)
        {
            statsBook = new InMemoryStatisticsData(name);
        }

        [Fact]
        public void PassParameterByValue()
        {
            var statsBook1 = GetStatsBook("SB 1");
            GetStatsBookSetName(statsBook1, "New Name");

            Assert.Equal("SB 1", statsBook1.Name);
        }

        private void GetStatsBookSetName(InMemoryStatisticsData statsBook, string name)
        {
            statsBook = new InMemoryStatisticsData(name);
        }

        [Fact]
        public void CanSetNameFromReference()
        {
            var statsBook1 = GetStatsBook("SB 1");
            SetName(statsBook1, "New Name");

            Assert.Equal("New Name", statsBook1.Name);
        }

        private void SetName(InMemoryStatisticsData statsBook, string name)
        {
            statsBook.Name = name;
        }

        [Fact]
        public void GetStatsBookReturnsDifferentObjects()
        {
            var statsBook1 = GetStatsBook("SB 1");
            var statsBook2 = GetStatsBook("SB 2");

            Assert.Equal("SB 1", statsBook1.Name);
            Assert.Equal("SB 2", statsBook2.Name);
            // Assert.NotSame(statsBook1, statsBook2);
        }

        [Fact]
        public void TwoVariablesCanReferenceSameObject()
        {
            var statsBook1 = GetStatsBook("SB 1");
            var statsBook2 = statsBook1;

            Assert.Same(statsBook1, statsBook2);
            Assert.True(Object.ReferenceEquals(statsBook1, statsBook2));
        }

        InMemoryStatisticsData GetStatsBook(string name)
        {
            return new InMemoryStatisticsData(name);
        }
    }
}
