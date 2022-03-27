using System;
using System.Threading;
using System.Threading.Tasks;

namespace _33.TaskCompletionSourceExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Task<int> task = Run(() =>
            {
                Thread.Sleep(5000);
                return 42;
            });

            Console.WriteLine(task.Result);
            Console.Read();
        }
        private static Task<TResult> Run<TResult>(Func<TResult> function)
        {
            var tcs = new TaskCompletionSource<TResult>();
            new Thread(() =>
            {
                try
                {
                    tcs.SetResult(function());
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            }).Start();
            return tcs.Task;
        }
    }


}
