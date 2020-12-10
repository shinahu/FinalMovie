using System;
using MovieApi.Models;
using Xunit;
using System.Data.SqlClient;




namespace MovieTest
{
    public class UnitTest1
    {
      Movies m = new Movies();
        
        // <<<< TEST Task >>>> //
        //a.Check that the num actors method provides the correct output.  
       [Fact]
        
        public void NumActorsTest()
        {
        int movieNo = 22;
        Assert.Equal(0,m.NumActors(movieNo));
        }
    
        [Theory]
        [InlineData(6,24)]
        [InlineData(4,70)]
        [InlineData(6,77)]
        [InlineData(2,78)]
        [InlineData(12,95)]
        public void TheoryTestingNumActors(int expected, int movieNo)
        {
            Assert.Equal(expected, m.NumActors(movieNo));
        }
        //b.Check that the GetAge method returns the correct output
        [Fact]
        public void GetAgeTest()
        {
            int movieNo = 20;
            Assert.NotEqual(20,m.GetAge(movieNo));
        }
        [Theory]
        [InlineData(15,21)]
        [InlineData(16,22)]
        [InlineData(20,70)]
        [InlineData(17,77)]
        
        public void TheoryTestingGetAge(int expected, int movieNo)
        {
            Assert.Equal(expected, m.GetAge(movieNo));
        }


    }
}