using System;
using System.Threading;
using System.Threading.Tasks;

namespace _57.BlockingVSSpinning
{
    class Program
    {
        private static bool _done;
        private static void MultiplyXByY(ref int val, int factor)
        {
            var spinWait = new SpinWait();
            while (true)
            {
                int snapshot1 = val;
                int calc = snapshot1 * factor;
                int snapshot2 = Interlocked.CompareExchange(ref val, calc, snapshot1);
                if (snapshot1 == snapshot2)
                {
                    return;
                }
                spinWait.SpinOnce();
            }
        }
        static void Main(string[] args)
        {
            Task.Run(() =>
            {
                try
                {
                    Console.WriteLine("Task started.");
                    Thread.Sleep(1000);
                    Console.WriteLine("Task is done.");
                }
                finally
                {
                    _done = true;
                }
            });

            SpinWait.SpinUntil(() =>
            {
                Thread.MemoryBarrier();
                return _done;
            });

            Console.WriteLine("The end of program");
            Console.Read();
        }
    }
}
