using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeCode
{
    class ExperimentsAbstracts
    {
        public static void Main(string[] args)
        {
            Base a = new Derived();
            a.nonABSMet();
            a.AbsMet();

            NonAbstractBase na= new DerivedFromNAB();
            na.nonABSMet();
            //Cannot use this as AbsMet is not defined in NonAbstractBase
            //na.AbsMet();

        }
    }

    abstract class Base
    {
        public void nonABSMet()
        {
            Console.WriteLine("Base non abstract method");
        }
        public abstract void AbsMet();
    }

    class NonAbstractBase
    {
        public void nonABSMet()
        {
            Console.WriteLine("Base non abstract class method");
        }
        public virtual void  VirtualMethod()
        {
            Console.WriteLine("Virtual method");
        }
    }

    class Derived : Base
    {
        public override void AbsMet()
        {
            Console.WriteLine("Derived abstract method");
        }
    }

    class DerivedFromNAB : NonAbstractBase
    {
        public void Met()
        {
            Console.WriteLine("Derived from non abstract class method");
        }


    }

}
