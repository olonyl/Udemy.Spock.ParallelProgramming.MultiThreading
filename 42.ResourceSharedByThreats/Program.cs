using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _42.ResourceSharedByThreats
{
    class Program
    {
        static void Main(string[] args)
        {
            CharacterLock c = new CharacterLock();

            var tasks = new List<Task>();
            for (int i = 0; i < 100; i++)
            {
                Task t1 = Task.Factory.StartNew(() =>
                 {
                     for (int j = 0; j < 10; j++)
                     {
                         c.Hit(10);
                     }
                 });
                tasks.Add(t1);

                Task t2 = Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 10; j++)
                    {
                        c.Heal(10);
                    }
                });
                tasks.Add(t2);
            }
            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"Resulting Health is this={c.Health}");

            Console.Read();
        }
    }
}
