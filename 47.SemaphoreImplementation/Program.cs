using System;
using System.Threading;
using System.Threading.Tasks;

namespace _47.SemaphoreImplementation
{
    class Program
    {
        public static SemaphoreSlim Bouncer { get; set; }
        static void Main(string[] args)
        {
            Bouncer = new SemaphoreSlim(3, 3);

            OpenNightClub();

            Console.Read();
        }

        private static void OpenNightClub()
        {
            for (int i = 1; i <= 50; i++)
            {
                var number = i;
                Task.Run(() => Guest(number));
            }
        }

        private static void Guest(int number)
        {
            Console.WriteLine($"Guest {number} is waiting to entering nightclub");
            Bouncer.Wait();

            Console.WriteLine($"Guest {number} is doing some dancing");
            Thread.Sleep(500);

            Console.WriteLine($"Guest {number} is leaving the nightclub");
            Bouncer.Release();
        }
    }
}
