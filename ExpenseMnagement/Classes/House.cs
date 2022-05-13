using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpenseManagement.Classes
{
    public interface IHouse
    {
        Member GetMember(string name);
        void MoveInMember(string name);
        void MoveOutMember(string name);
        List<Member> GetMembers();
    }

    public class House : IHouse
    {
        public const int MAX_MEMBERS = 3;
        public List<Member> members = new List<Member>();

        public void MoveInMember(string name)
        {
            if (members.Count < MAX_MEMBERS)
            {
                var newMember = new Member { Name = name };
                foreach (var member in members)
                {
                    newMember.Dues.Add(member, 0);//Add new members to the new member's due list with due amount 0
                    member.Dues.Add(newMember, 0);
                }
                members.Add(newMember);
                Console.WriteLine("SUCCESS");
            }
            else
                Console.WriteLine("HOUSEFUL");
        }

        public void MoveOutMember(string name)
        {
            Member memberToRemove = members.Find(x => x.Name == name);
            if (memberToRemove != null)
            {
                if (memberToRemove.Dues.Values.ToList().Find(x => x > 0) > 0)//check if this member has any outstanding dues
                {
                    Console.WriteLine("FAILURE");
                    return;
                }

                foreach (var member in members)
                {
                    if (member == memberToRemove)
                        continue;

                    if (member.Dues[memberToRemove] > 0)//check if any dues are owed to this member
                    {
                        Console.WriteLine("FAILURE");
                        return;
                    }
                    member.Dues.Remove(memberToRemove);// remove the removed member from existing member's due lists
                }

                members.Remove(memberToRemove);
                Console.WriteLine("SUCCESS");
            }
            else
                Console.WriteLine("MEMBER_NOT_FOUND");
        }

        public Member GetMember(string name)
        {
            var member = members.Find(x => x.Name == name);
            if (member != null)
                return member;

            return null;
        }

        public List<Member> GetMembers()
        {
            return members;
        }
    }
}
