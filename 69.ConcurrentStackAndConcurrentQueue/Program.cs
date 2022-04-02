using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace _69.ConcurrentStackAndConcurrentQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcurrentQueueDemo();
            ConcurrentStackDemo();
            ConcurrentBagDemo();
            Console.ReadLine();
        }

        private static void ConcurrentBagDemo()
        {
            var names = new ConcurrentBag<string>();

            names.Add("Olonyl");
            names.Add("Horacio");
            names.Add("Landeros");

            Console.WriteLine($"After Add in Bag, count = {names.Count}");

            string item1; //=names.TryTake();
            bool successs = names.TryTake(out item1);
            if (successs)
            {
                Console.WriteLine("\nRemoving " + item1);
            }
            else
            {
                Console.WriteLine("bag was empty");
            }

            string item2;//=names.Peek();
            successs = names.TryPeek(out item2);
            if (successs)
            {
                Console.WriteLine("Peeking " + item2);
            }
            else
            {
                Console.WriteLine("bag was empty");
            }
            Console.WriteLine("Enumerating Bag");

            PrintOutCollection(names);

            Console.WriteLine("\nAfter enumerating bag, count = " + names.Count);
        }

        private static void ConcurrentQueueDemo()
        {
            var names = new ConcurrentQueue<string>();

            names.Enqueue("Olonyl");
            names.Enqueue("Horacio");
            names.Enqueue("Landeros");

            Console.WriteLine($"After enqueuing, count = {names.Count}");

            string item1; //=names.Dequeue();
            bool successs = names.TryDequeue(out item1);
            if (successs)
            {
                Console.WriteLine("\nRemoving " + item1);
            }
            else
            {
                Console.WriteLine("queue was empty");
            }

            string item2;//=names.Peek();
            successs = names.TryPeek(out item2);
            if (successs)
            {
                Console.WriteLine("Peeking " + item2);
            }
            else
            {
                Console.WriteLine("queue was empty");
            }
            Console.WriteLine("Enumerating Queue");

            PrintOutCollection(names);

            Console.WriteLine("\nAfter enumerating queue, count = " + names.Count);
        }

        private static void ConcurrentStackDemo()
        {
            var names = new ConcurrentStack<string>();

            names.Push("Olonyl");
            names.Push("Horacio");
            names.Push("Landeros");

            Console.WriteLine($"After stacking, count = {names.Count}");

            string item1; //=names.TryPop();
            bool successs = names.TryPop(out item1);
            if (successs)
            {
                Console.WriteLine("\nRemoving " + item1);
            }
            else
            {
                Console.WriteLine("stack was empty");
            }

            string item2;//=names.Peek();
            successs = names.TryPeek(out item2);
            if (successs)
            {
                Console.WriteLine("Peeking " + item2);
            }
            else
            {
                Console.WriteLine("stack was empty");
            }
            Console.WriteLine("Enumerating Stack");

            PrintOutCollection(names);

            Console.WriteLine("\nAfter enumerating stack, count = " + names.Count);
        }


        private static void PrintOutCollection<T>(IEnumerable<T> names)
        {
            foreach (var item in names)
            {
                Console.WriteLine(item);
            }
        }
    }
}
