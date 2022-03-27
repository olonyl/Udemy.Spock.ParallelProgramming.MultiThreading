using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace _26.CancellingATask
{
    class WebClientWrapper
    {
        private static WebClient wc = new WebClient();
        public static async Task LongRunningOperation(CancellationToken ct)
        {
            if (!ct.IsCancellationRequested)
            {
                using (CancellationTokenRegistration ctr = ct.Register(() => { wc.CancelAsync(); }))
                {
                    wc.DownloadStringAsync(new Uri("https://google.com"));
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var parentCts = new CancellationTokenSource();
            var childCts = CancellationTokenSource.CreateLinkedTokenSource(parentCts.Token);
            var cts = new CancellationTokenSource();

            var t1 = Task.Run(() => Print(true, parentCts.Token), parentCts.Token);
            var t2 = Task.Run(() => Print(false, childCts.Token), childCts.Token);
            var t3 = Task.Run(() => WebClientWrapper.LongRunningOperation(cts.Token), cts.Token);

            //Thread.Sleep(10);
            parentCts.CancelAfter(10);
            cts.Cancel();
            try
            {

                Console.WriteLine($"The first task processed:{t1.Result}");
                Console.WriteLine($"The second task processed:{t2.Result}");
            }
            catch (AggregateException)
            {

            }

            Console.WriteLine($"T1:{t1.Status}");
            Console.WriteLine($"T2:{t2.Status}");
            Console.WriteLine($"T3:{t3.Status}");

            Console.Read();
        }

        private static int Print(bool isEven, CancellationToken token)
        {
            var total = 0;
            Console.WriteLine($"Is thread pool thread:{Thread.CurrentThread.IsThreadPoolThread}");
            if (isEven)
            {
                for (int i = 0; i < 1000; i++)
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
                for (int i = 0; i < 1000; i++)
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
