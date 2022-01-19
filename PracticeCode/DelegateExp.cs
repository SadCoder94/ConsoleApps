using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeCode
{
    class DelegateExp
    {
        public delegate void addNum(int a, int b);

        public delegate int someDele1(int a, int b);
        public void Sum(int a, int b) 
        { 
            Console.WriteLine(a + b); 
        }

        public void Mul(int a, int b)
        {
            Console.WriteLine(a * b);
        }


        public int somFu(int a, int b)
        {
            return a + b;
        }

        public static void Main(string[] args)
        {
            var obj = new DelegateExp();

            addNum add = new addNum(obj.Sum);
            someDele1 somd = new someDele1(obj.somFu);
            add += obj.Mul;

            add(1, 2);
            var asa = somd(1, 2);
            Console.WriteLine(asa);
        }
    }
}
