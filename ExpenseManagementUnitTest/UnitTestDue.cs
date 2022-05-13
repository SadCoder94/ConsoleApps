using ExpenseManagement.Classes;
using System;
using Xunit;

namespace ExpenseManagementUnitTest
{
    public class UnitTestDue
    {
        [Fact]
        public void AddingSpends()
        {
            House newHouse = Setup();
            DueCalculator dueCalculator = new DueCalculator(newHouse);
            dueCalculator.Spend("SPEND 6000 Sam Holt Drake");
            var member = newHouse.GetMember("Holt");
            var memberOwedTo = newHouse.GetMember("Sam");

            Assert.Equal(2000, member.Dues[memberOwedTo]);
        }

        static House Setup()
        {
            House newHouse = new House();
            newHouse.MoveInMember("Sam");
            newHouse.MoveInMember("Holt");
            newHouse.MoveInMember("Drake");
            return newHouse;
        }
    }
}
