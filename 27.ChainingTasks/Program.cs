using System;
using System.Threading;
using System.Threading.Tasks;

namespace _27.ChainingTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var cts2 = new CancellationTokenSource();

            var t1 = Task.Run(() => Print(true, cts.Token), cts.Token);
            var t2 = t1.ContinueWith((prevtask) =>
              {
                  Console.WriteLine($"How many numbers were processed by previous task={prevtask.Result}");
                  Task.Run(() => Print(false, cts.Token), cts.Token);
              }, TaskContinuationOptions.OnlyOnRanToCompletion);

            t1.ContinueWith(t =>
                {
                    Console.WriteLine("Finally, we are here");
                }
            , TaskContinuationOptions.OnlyOnFaulted);

            Console.WriteLine("Main thread is not blocked");

            var t3 = Task.Run(() => Print(true, cts2.Token), cts2.Token);
            var t4 = Task.Run(() => Print(true, cts2.Token), cts2.Token);

            Task.Factory.ContinueWhenAll(new[] { t3, t4 }, tasks =>
            {
                var t1Task = tasks[0];
                var t2Task = tasks[1];

                Console.WriteLine($"t1Task={t1Task.Result}, t2Task={t2Task.Result}");
            });


            Console.Read();
        }

        private static int Print(bool isEven, CancellationToken token)
        {
            var total = 0;
            Console.WriteLine($"Is thread pool thread:{Thread.CurrentThread.IsThreadPoolThread}");
            if (isEven)
            {
                throw new InvalidOperationException();
                for (int i = 0; i < 20; i++)
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
                for (int i = 0; i < 20; i++)
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
