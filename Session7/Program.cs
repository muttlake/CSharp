using System;

namespace Session7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            var dir = System.IO.Directory.GetCurrentDirectory();
            var file = System.IO.Path.Combine(dir, "File.txt");
            var content = "how now brown cow?";

            //write
            System.IO.File.WriteAllText(file, content);

            //read
            var read = System.IO.File.ReadAllText(file);
            Trace.Assert(read.Equals(content));


        }


    }
}
