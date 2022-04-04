using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace _77.PLINQExample
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<int> numbers = Enumerable.Range(3, 100000 - 3);

            var parallelQuery =
                from n in numbers.AsParallel()
                where Enumerable.Range(2, (int)Math.Sqrt(n)).All(i => n % i > 0)
                select n;

            int[] primes = parallelQuery.ToArray();

            primes.ToList().AsParallel()
                .Where(n => n > 100)
                .AsParallel()
                .Select(n => n * n);

            parallelQuery =
                from n in numbers.AsParallel().AsOrdered()
                where Enumerable.Range(2, (int)Math.Sqrt(n)).All(i => n % i > 0)
                select n;

            var result = from site in new[]
            {
                "www.google.com",
                "www.udemy.com",
                "www.reddit",
                "www.facebook.com",
                "www.stackoverflow.com",
                "www.pluralshight.com"
            }
            .AsParallel()
            .WithDegreeOfParallelism(6)
                         let p = new Ping().Send(site)
                         select new
                         {
                             site,
                             Result = p.Status,
                             Time = p.RoundtripTime
                         };


            Console.Read();
        }
    }
}
