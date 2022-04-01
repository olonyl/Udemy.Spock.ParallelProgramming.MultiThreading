using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace _67.ImmutableDictionaryExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var dic = ImmutableDictionary<int, string>.Empty;

            dic = dic.Add(1, "John");
            dic = dic.Add(2, "Alex");
            dic = dic.Add(3, "April");

            PrintCollection(dic);

            Console.WriteLine("Changing value of key 2 to Bob");
            dic = dic.SetItem(2, "Bob");

            PrintCollection(dic);

            var april = dic[3];
            Console.WriteLine($"Who is 3: {april}");

            Console.WriteLine("Remove record where key = 2");
            dic = dic.Remove(2);

            PrintCollection(dic);

            Console.ReadLine();
        }

        static void PrintCollection<T>(IEnumerable<T> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
}
