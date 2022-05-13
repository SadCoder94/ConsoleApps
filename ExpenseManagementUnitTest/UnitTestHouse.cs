using ExpenseManagement.Classes;
using System;
using Xunit;

namespace ExpenseManagementUnitTest
{
    public class UnitTestHouse
    {
        [Fact]
        public void AddingMembers()
        {
            House newHouse = Setup();
            Assert.Equal(3, newHouse.GetMembers().Count);
        }

        [Fact]
        public void MoveoutMembers()
        {
            House newHouse = Setup();

            Assert.Equal(3, newHouse.GetMembers().Count);

            newHouse.MoveOutMember("Sam");
            Assert.Equal(2, newHouse.GetMembers().Count);

        }

        [Fact]
        public void MoveoutMembersWithDues()
        {
            House newHouse = Setup();
            var member = newHouse.GetMember("Sam");
            var memberDueFor = newHouse.GetMember("Holt");
            member.Dues[memberDueFor] = 10;

            Assert.Equal(3, newHouse.GetMembers().Count);

            newHouse.MoveOutMember("Sam");
            Assert.Equal(3, newHouse.GetMembers().Count);

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
