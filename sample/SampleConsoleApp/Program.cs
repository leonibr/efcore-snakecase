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
            Console.WriteLine("Sample app does nothing really...");
            DoCommand("dotnet", "ef migrations remove");
            Thread.Sleep(200);
            DoCommand("dotnet", "build");
            Thread.Sleep(200);

            Console.WriteLine("Creating Migration 'Initial'");

            DoCommand("dotnet", "ef migrations add \"Initial\"");
            Thread.Sleep(200);
            Console.WriteLine("Generating Database script for SQLite");
            DoCommand("dotnet", "ef migrations script --output sample.sql");
            Thread.Sleep(200);
            DoCommand("type", "sample.sql");
            Console.WriteLine("sample has finished..");
            //Console.ReadLine();
        }


        static void DoCommand(string command, string args)
        {
            var process = new Process();
            var startInfo = new ProcessStartInfo(command, args);
            //startInfo = startInfo;
            startInfo.FileName = command;
            startInfo.Arguments = args;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.OutputDataReceived += (sender, data) =>
            {
                Console.WriteLine(data.Data);
            };
            process.StartInfo.RedirectStandardError = true;
            process.ErrorDataReceived += (sender, data) =>
            {
                Console.WriteLine(data.Data);
            };
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        }
    }
}
