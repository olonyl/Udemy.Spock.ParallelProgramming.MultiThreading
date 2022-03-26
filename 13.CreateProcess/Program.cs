using System;
using System.Diagnostics;

namespace _13.CreateProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            var filename = @"C:\temp\HelloWorld.txt";
            var progname = "notepad.exe";

            Process.Start(progname, filename);
            Process.Start(filename);

            var app = new Process
            {
                StartInfo =
                {
                    FileName=progname,
                    Arguments=filename
                }
            };
            app.Start();
            app.PriorityClass = ProcessPriorityClass.RealTime;

            app.WaitForExit();

            Console.WriteLine("No more waiting");

            var process = Process.GetProcesses();
            foreach (var proc in process)
            {
                if (proc.ProcessName == "notepad")
                {
                    proc.Kill();
                }
            }
            Console.Read();
        }
    }
}
