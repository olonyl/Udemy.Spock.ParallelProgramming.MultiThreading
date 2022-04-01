using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace _66.ImmutableSetExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var hashSet = ImmutableHashSet<int>.Empty;
            hashSet = hashSet.Add(5);
            hashSet = hashSet.Add(10);

            PrintOutCollection(hashSet);

            Console.WriteLine($"Remove 5");
            hashSet = hashSet.Remove(5);

            PrintOutCollection(hashSet);

            Console.WriteLine($"---ImmutableSortedSet Demo---");
            var sortedSet = ImmutableSortedSet<int>.Empty;
            sortedSet = sortedSet.Add(10);
            sortedSet = sortedSet.Add(5);

            PrintOutCollection(sortedSet);

            var smallest = sortedSet[0];
            Console.WriteLine($"Smallest Item:{smallest}");

            Console.WriteLine("Remove 5");
            sortedSet = sortedSet.Remove(5);

            PrintOutCollection(sortedSet);

            Console.Read();
        }

        private static void PrintOutCollection(IEnumerable<int> hashSet)
        {
            foreach (var item in hashSet)
            {
                Console.WriteLine(item);
            }
        }
    }
}
