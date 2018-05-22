using System;

namespace Session5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class Person
    {
        public override void SetName(string value)
        {
            if (string.isNullOrWhiteSpace(value))
                throw new ArgumentNullException("value");
        }
    }
}
