using System;
using System.Threading;
using System.Threading.Tasks;

namespace _49.DeadLock
{
    class Program
    {
        static readonly object firstLock = new object();
        static readonly object secondLock = new object();

        static void Main(string[] args)
        {
            Task.Run(Do);

            AcquireInversed();
            Console.Read();
        }

        private static void AcquireInversed()
        {
            Thread.Sleep(500);
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}-Locking secondLock");
            lock (secondLock)
            {
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}-Locked secondLock");
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}-Locking firstLock");
                lock (firstLock)
                {
                    Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}-Locked firstLock");
                }
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}-Released firstLock");
            }
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}-Released secondLock");
        }

        private static void Do()
        {
            Console.WriteLine($"\t\t\t\t{Thread.CurrentThread.ManagedThreadId}-Locking firstLock");
            lock (firstLock)
            {
                Console.WriteLine($"\t\t\t\t{Thread.CurrentThread.ManagedThreadId}-Locked firstLock");

                Thread.Sleep(1000);

                Console.WriteLine($"\t\t\t\t{Thread.CurrentThread.ManagedThreadId}-Locking secondLock");
                lock (secondLock)
                {
                    Console.WriteLine($"\t\t\t\t{Thread.CurrentThread.ManagedThreadId}-Locked secondLock");
                }
                Console.WriteLine($"\t\t\t\t{Thread.CurrentThread.ManagedThreadId}-Released secondLock");
            }
            Console.WriteLine($"\t\t\t\t{Thread.CurrentThread.ManagedThreadId}-Released fisrtLock");
        }
    }
}
