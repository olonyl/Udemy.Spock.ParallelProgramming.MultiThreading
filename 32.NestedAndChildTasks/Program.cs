using System;
using System.Threading;
using System.Threading.Tasks;

namespace _32.NestedAndChildTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Factory.StartNew(() =>
            {
                Task nested = Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("hello world!");
                    Thread.Sleep(4000);
                });

            }, TaskCreationOptions.AttachedToParent).Wait();

            Console.WriteLine("Main");
            Console.ReadLine();
        }
    }
}
