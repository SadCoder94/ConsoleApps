using System;

namespace PracticeCode
{
    class ShallowCopy: ICloneable

    {

        public int I { get; set; }

        public int J { get; set; }
        public ReferenceType Reference = new ReferenceType();

        public object Clone()//Deep Copying
        {
            ShallowCopy newSC = (ShallowCopy)this.MemberwiseClone();

            ReferenceType newRef = new ReferenceType();
            newRef.SomeProperty = this.Reference.SomeProperty;
            newSC.Reference = newRef;

            return newSC;
        }
    }
    class ReferenceType
    {
        public string SomeProperty { get; set; }
    }

    class Demo

    {

        public static void Main(string[] args)
        {
            ShallowCopy obj = new ShallowCopy();

            obj.Reference.SomeProperty = "Changing original";
            ShallowCopy objClone = (ShallowCopy)obj.Clone();
            
            obj.Reference.SomeProperty = "Changing after Cloning";// setting obj value after cloning..

            Console.WriteLine($"objvalue: { obj.Reference.SomeProperty} \t Clone value: {objClone.Reference.SomeProperty}");

        }
    }
}
