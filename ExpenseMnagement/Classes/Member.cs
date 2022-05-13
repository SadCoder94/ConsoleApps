using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseManagement.Classes
{
    public class Member
    {
        public string Name { get; set; }

        public Dictionary<Member,int> Dues { get; set; }

        public Member()
        {
            Dues = new Dictionary<Member, int>();
        }

        public Dictionary<Member, int> GetDues()
        {
            return Dues;
        }
    }
}
