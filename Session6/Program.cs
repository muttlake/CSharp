using System;

namespace Session6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            // var perfCounter = new PerformanceCounter
            // {
            //     CategoryName = category,
            //     CounterName = counter,
            //     MarchineName = machine,
            //     ReadOnly = false,
            // };

            // perfCounter.IncrementBy(10);

            Console.WriteLine(Run1());
            Console.WriteLine(Run2());
            Console.WriteLine(Run3());
            Console.WriteLine(Run4());
            Console.WriteLine(Run5());
        }

        static Int64 Run1() { return Run(10000); }
        static Int64 Run2() { return Run(30000); }
        static Int64 Run3() { return Run(40000); }
        static Int64 Run4() { return Run(25000); }
        static Int64 Run5() { return Run(35000); }

        static Int64 Run(int num)
        {
            Int64 x = 0;

            for(int i1 = 0; i1 < num; i1++)
                for(int i2 = 0; i2 < num; i2++)
                    x += i2;

            return x;
        }


    }
}
