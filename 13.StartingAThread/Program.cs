using System;
using System.Threading;

namespace _13.StartingAThread
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread t1 = new Thread(() => Print(true));
            t1.Start();

            Print(true);

            Console.Read();
        }
        private static void Print(bool isEven)
        {
            Console.WriteLine($"Current Thread ID: {Thread.CurrentThread.ManagedThreadId}");
            if (isEven)
            {
                for (int i = 0; i < 100; i++)
                {
                    if (i % 2 == 0 && isEven)
                    {
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
                        Console.WriteLine(i);
                    }
                }
            }

        }
    }

}
