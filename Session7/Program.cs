using System;
using System.Diagnostics;
using System.IO.IsolatedStorage;

namespace Session7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            var dir = System.IO.Directory.GetCurrentDirectory();
            //var dir2 = System.IO.Directory.EnumerateDirectories();
            var file = System.IO.Path.Combine(dir, "File.txt");
            var content = "how now brown cow?";

            //write
            System.IO.File.WriteAllText(file, content);

            //read
            var read = System.IO.File.ReadAllText(file);
            Trace.Assert(read.Equals(content));

            // special folders
            var docs = Environment.SpecialFolder.MyDocuments;
            Console.WriteLine(docs);
            var app = Environment.SpecialFolder.CommonApplicationData;
            var prog = Environment.SpecialFolder.ProgramFilesX86;
            var desk = Environment.SpecialFolder.Desktop.ToString();

            var file2 = System.IO.Path.Combine(dir, "FileBEAN.txt");
            var content2 = "BEABEBBBEBBEBBEAN";
            System.IO.File.WriteAllText(file2, content2);

            // application folder
            var dir3 = System.IO.Directory.GetCurrentDirectory();

            // // isolated storage path
            // var iso = IsolatedStorageFile
            //             .GetStore(IsolatedStorageScope.Assembly, "Demo")
            //             .GetDirectoryNames("*");

            // Gets metadata for a directory
            //var temp = new System.IO.DirectoryInfo("/c/Revature");


            foreach(var item in System.IO.Directory.GetFiles(dir3))
            {
                Console.WriteLine(System.IO.Path.GetFileName(item));
            }
            
            Console.WriteLine(dir3);

            // rename/move
            var path1 = @"C:\Revature\CSharp\Session7\File.txt";
            var path2 = @"C:\Revature\CSharp\Session7\fileOROO.txt";
            System.IO.File.Move(path1, path2);

            //file info
            var info = new System.IO.FileInfo(path2);
            Console.WriteLine("{0}kb", info.Length/1000);










        }





    }
}
