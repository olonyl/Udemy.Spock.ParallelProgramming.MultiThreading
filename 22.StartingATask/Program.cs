using System;
using System.Threading;
using System.Threading.Tasks;

namespace _22.StartingATask
{
    class Program
    {
        static void Main(string[] args)
        {
            Task<int> t1 = Task.Factory.StartNew(() => Print(true)
            , CancellationToken.None
            , TaskCreationOptions.DenyChildAttach | TaskCreationOptions.LongRunning
            , TaskScheduler.Default);

            Task<int> t2 = Task.Factory.StartNew(() => Print(false));


            Console.WriteLine($"Task #1 Result={t1.Result}");
            Console.WriteLine($"Task #2 Result={t2.Result}");

            Console.ReadLine();
        }

        private static int Print(bool isEven)
        {
            Console.WriteLine($"Task {Task.CurrentId} Is Thread Pool Thread= {Thread.CurrentThread.IsThreadPoolThread}");
            int total = 0;
            if (isEven)
            {
                for (int i = 0; i < 100; i++)
                {
                    if (i % 2 == 0)
                    {
                        total++;
                        Console.WriteLine($"Current task id={Task.CurrentId}, Value={i}");
                    }
                }
            }
            else
            {
                for (int i = 0; i < 100; i++)
                {
                    if (i % 2 != 0)
                    {
                        total++;
                        Console.WriteLine($"Current task id={Task.CurrentId}, Value={i}");
                    }
                }
            }
            return total;
        }
    }

}
