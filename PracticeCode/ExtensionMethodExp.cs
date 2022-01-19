using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeCode
{
    public class ExtensionMethodExp
    {
        public static void Main(string[] args)
        {
            var vA = new A1();
            vA.Met1();
            vA.Met2();
            vA.Met3();
            vA.Met4();
        }
    }

    public class A1
    {
        public void Met1() { Console.WriteLine("A1 Method 1"); }
        public void Met2() { Console.WriteLine("A1 Method 2"); }
    }

    public static class A2
    {
        public static void Met3(this A1 a) { Console.WriteLine("A2 Extension Method 3"); }
        public static void Met4(this A1 a) { Console.WriteLine("A1 Extension Method 4"); }
    }
}
