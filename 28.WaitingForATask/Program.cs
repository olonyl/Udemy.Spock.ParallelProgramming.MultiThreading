using System;
using System.Threading;
using System.Threading.Tasks;

namespace _28.WaitingForATask
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();

            var t1 = Task.Run(() => Print(true, cts.Token), cts.Token);
            var t2 = Task.Run(() => Print(false, cts.Token), cts.Token);

            Console.WriteLine("Started t1");
            Console.WriteLine("Started t2");

            var tr = Task.WhenAny(t1, t2);
            tr.ContinueWith(x =>
            {
                Console.WriteLine($"The id of a task which completed first={tr.Result.Id}");
            });
            Console.WriteLine("After when any");
            //int result = Task.WaitAny(t1, t2);
            //Task.WaitAll(t1, t2);
            //t1.Wait();

            Console.WriteLine("Finished t1");
            Console.WriteLine("Finished t2");

            Console.Read();
        }

        private static int Print(bool isEven, CancellationToken token)
        {
            var total = 0;
            var maxValue = 100;

            Console.WriteLine($"Is thread pool thread:{Thread.CurrentThread.IsThreadPoolThread}");
            if (isEven)
            {
                for (int i = 0; i < maxValue; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Cancellation Requested");
                    }
                    token.ThrowIfCancellationRequested();
                    if (i % 2 == 0)
                    {
                        total++;
                        Console.WriteLine($"Current task id={Task.CurrentId}, Value={i}");
                    }
                }
            }
            else
            {
                for (int i = 0; i < maxValue; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Cancellation Requested");
                    }
                    token.ThrowIfCancellationRequested();
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
