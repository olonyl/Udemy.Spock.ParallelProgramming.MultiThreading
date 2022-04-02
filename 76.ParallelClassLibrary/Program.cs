using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace _76.ParallelClassLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            UserParallelForEach();

            Console.Read();

        }

        private static void UserParallelForEach()
        {
            string sentence = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus " +
                "hendrerit metus vitae nulla sollicitudin placerat. Aliquam tempus neque in nunc convallis, vel facilisis " +
                "neque malesuada. Nunc ornare ante a congue faucibus. Cras euismod molestie convallis. " +
                "Curabitur ut blandit nulla. Nam scelerisque scelerisque sem ut finibus. Duis sodales sodales tempus. " +
                "Nulla fringilla tellus in lacus dapibus dapibus. Pellentesque tortor diam, vehicula at iaculis non, " +
                "pellentesque quis arcu. Pellentesque ac molestie eros. In porta ex quis sollicitudin hendrerit. " +
                "Nunc rutrum urna ut tincidunt elementum. Nulla a vulputate urna. Suspendisse potenti.";

            string[] words = sentence.Split(' ');

            Parallel.ForEach(words, word =>
            {
                Console.WriteLine($"\"{word}\" is of {word.Length} length = thread {Thread.CurrentThread.ManagedThreadId}");
            });
        }

        private static void UserParallelFor()
        {
            ParallelOptions po = new ParallelOptions();

            Parallel.For(1, 11, i => { Console.WriteLine($"{i * i * i}"); });
        }

        public void UseParallel()
        {
            var data = new List<int>();

            Parallel.Invoke(
                () => data.AddRange(RunLoop1()),
                () => data.AddRange(RunLoop2())
                );

            Console.WriteLine($"Elements: {data.Count}");
            foreach (var item in data)
            {
                Console.WriteLine(item);
            }
        }
        static IEnumerable<int> RunLoop1()
        {
            for (int i = 0; i < 100; i++)
            {
                //Console.WriteLine($"ThreadID:{Thread.CurrentThread.ManagedThreadId};Iteration:{i}");
                yield return i;
            }
            Thread.Sleep(2000);
        }
        static IEnumerable<int> RunLoop2()
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(50);
                //Console.WriteLine($"ThradID:{Thread.CurrentThread.ManagedThreadId};Iteration:{i}");
                yield return i;
            }
            Thread.Sleep(3000);
        }
    }
}
