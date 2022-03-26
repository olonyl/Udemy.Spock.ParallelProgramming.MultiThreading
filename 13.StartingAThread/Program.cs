using System;
using System.Threading;

namespace _13.StartingAThread
{
    public class PrintingInfo
    {
        public int ProcessedNumbers { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var printInfo = new PrintingInfo();

            Thread t1 = new Thread(() => Print(true, printInfo));
            t1.Start();

            if (t1.Join(TimeSpan.FromMilliseconds(5000)))
            {
                Console.WriteLine($"I'm sure that spawned thread " +
                                    $"processed that many: {printInfo.ProcessedNumbers}");
            }
            else
            {
                Console.WriteLine("Timed out. Can't process results");
            }

            Thread.Sleep(10);

            t1.Abort();

            Console.WriteLine("After abort");
            Console.ReadLine();

            Print(true, printInfo);

            Console.ReadLine();
        }
        private static void Print(bool isEven, PrintingInfo printingInfo)
        {
            //try
            //{
            Console.WriteLine($"Current Thread ID: {Thread.CurrentThread.ManagedThreadId}");
            if (isEven)
            {
                for (int i = 0; i < 100; i++)
                {
                    if (i % 2 == 0 && isEven)
                    {
                        printingInfo.ProcessedNumbers++;
                        Console.WriteLine(i);
                    }
                }
            }
            else
            {
                for (int i = 0; i < 100; i++)
                {
                    if (i % 2 != 0)
                    {
                        printingInfo.ProcessedNumbers++;
                        Console.WriteLine(i);
                    }
                }
            }
            //}
            //catch (ThreadAbortException ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

        }
    }

}
