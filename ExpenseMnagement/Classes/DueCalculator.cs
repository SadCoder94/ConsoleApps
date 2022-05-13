using System;
using System.Linq;

namespace ExpenseManagement.Classes
{
    public interface IDueCalculator
    {
        void ClearDues(string command);
        void Dues(string memberName);
        void Spend(string spendCommand);
    }

    public class DueCalculator : IDueCalculator
    {
        private IHouse _house;
        public DueCalculator(IHouse house)
        {
            _house = house;
        }

        public void Spend(string spendCommand)
        {
            string[] commandValues = spendCommand.Split(' ');
            var amount = commandValues[1];

            var spentByMemberName = commandValues[2];
            Member spentByMember = _house.GetMember(spentByMemberName);
            if (spentByMember == null)
            {
                Console.WriteLine("MEMBER_NOT_FOUND");
                return;
            }

            var totalParticipants = commandValues.Length - 2;
            var perHeadExpense = int.Parse(amount) / totalParticipants;
            bool successfulOp = false;

            for (int i = 3; i < commandValues.Length; i++)
            {
                var memberDueFor = _house.GetMember(commandValues[i]);
                if (memberDueFor != null)
                {
                    balanceDues(memberDueFor, spentByMember, perHeadExpense);
                    successfulOp = true;
                }
                else
                    successfulOp = false;
            }

            if (successfulOp)
                Console.WriteLine("SUCCESS");
            else
                Console.WriteLine("MEMBER_NOT_FOUND");

        }
        
        private void balanceDues(Member owedBy, Member owedTo, int perHeadExpense)
        {                               
            var houseMembers = _house.GetMembers();
            var commonMembers = houseMembers.Where(x => x.Name != owedBy.Name && x.Name != owedTo.Name);//get members both members may have dues for

            var DuesOfmemberOwedBy = owedBy.GetDues();
            var DuesOfMemberOwedTo = owedTo.GetDues();
            owedBy.Dues[owedTo] += perHeadExpense;
            
            //find smallest non-zero due amount to balance
            var smallestDueValueForOwedBy = DuesOfmemberOwedBy.Select(x => x.Value).Min();
            var smallestDueValueForOwedTo = DuesOfMemberOwedTo.Select(x => x.Value).Min();
            var smallestDue = smallestDueValueForOwedBy < smallestDueValueForOwedTo ? smallestDueValueForOwedBy : smallestDueValueForOwedTo;

            if (smallestDue == 0 & smallestDueValueForOwedBy != 0)
            {
                smallestDue = smallestDueValueForOwedBy;
            }
            if (smallestDue == 0 & smallestDueValueForOwedTo != 0)
            {
                smallestDue = smallestDueValueForOwedTo;
            }

            foreach (var commonMember in commonMembers)
            {
                owedBy.Dues[commonMember] += smallestDue;
                owedBy.Dues[owedTo] -= smallestDue;
                owedTo.Dues[commonMember] -= smallestDue;
            }
        }

        public void Dues(string memberName)
        {
            var member = _house.GetMember(memberName);
            if (member == null)
            {
                Console.WriteLine("MEMBER_NOT_FOUND");
                return;
            }
            foreach (var due in member.Dues)
            {
                Console.WriteLine(due.Key.Name + " " + due.Value);
            }
        }

        public void ClearDues(string command)
        {
            string[] commandValues = command.Split(' ');
            var paidBy = _house.GetMember(commandValues[1]);
            var paidTo = _house.GetMember(commandValues[2]);
            var amount = int.Parse(commandValues[3]);

            if (paidBy == null || paidTo == null)
            {
                Console.WriteLine("MEMBER_NOT_FOUND");
                return;
            }

            if (paidBy.Dues[paidTo] < amount)
                Console.WriteLine("INCORRECT_PAYMENT");
            else
            {
                paidBy.Dues[paidTo] -= amount;
                Console.WriteLine(paidBy.Dues[paidTo]);
            }

        }
    }
}
