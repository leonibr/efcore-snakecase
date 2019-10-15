using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace SampleConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sample app does nothing...\n 1. Remove previuos migrations");
            Console.WriteLine("> dotnet ef migrations remove");
            
            Console.WriteLine("> dotnet build");            

            Console.WriteLine("\n2. Creating Migration 'Initial'");

            Console.WriteLine("> dotnet ef migrations add \"Initial\"");
            Thread.Sleep(200);
            Console.WriteLine("\n3. Generating Database script for SQLite");
            Console.WriteLine("> dotnet ef migrations script --output sample.sql");
            Thread.Sleep(200);
            Console.WriteLine("check the generated file sample.sql");
           
            //Console.ReadLine();
        }



    }
}
