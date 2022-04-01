using System;
using System.Collections.Immutable;

namespace _65.ImmutableListExample
{
    class Program
    {
        static void Main(string[] args)
        {
            ListDemo();
            Console.Read();
        }
        static void ListDemo()
        {
            var list = ImmutableList<int>.Empty;
            list = list.Add(2);
            list = list.Add(3);
            list = list.Add(4);
            list = list.Add(5);

            PrintOutCollection(list);

            Console.WriteLine("Remove 4 and then Remove at index=2");

            list = list.Remove(4);
            list = list.RemoveAt(2);

            PrintOutCollection(list);

            Console.WriteLine("Insert 1 at 0 and 4 at 3");
            list = list = list.Insert(0, 1);
            list = list.Insert(3, 4);

            PrintOutCollection(list);

        }

        private static void PrintOutCollection(ImmutableList<int> list)
        {
            foreach (var element in list)
            {
                Console.WriteLine(element);
            }
        }
    }
}
