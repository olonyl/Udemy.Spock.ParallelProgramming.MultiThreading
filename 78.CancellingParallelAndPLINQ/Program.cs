using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace _78.CancellingParallelAndPLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            IEnumerable<CreditCard> cards = Enumerable.Range(1, 5)
                .Select(value =>
                    new CreditCard
                    {
                        Id = value,
                        Liabilities = random.Next(800, 1900)
                    });

            CancellationTokenSource cts = new CancellationTokenSource();
            var task = Task.Run(() =>
            {
                try
                {
                    //UsingAsParallel(cards, cts);

                    //UsingParallelForEach(cards, cts);

                    UsingParallelForEachWithLoopState(random, cards, cts);
                }
                catch (OperationCanceledException ex)
                {
                    Console.WriteLine("Canceled!");
                }
            });

            Thread.Sleep(1500);
            Console.WriteLine("Canceling the operation");
            cts.Cancel();

            Console.Read();
        }

        private static void UsingParallelForEachWithLoopState(Random random, IEnumerable<CreditCard> cards, CancellationTokenSource cts)
        {
            Parallel.ForEach(cards
                , new ParallelOptions { CancellationToken = cts.Token }
                , (curItem, loopState, curIndex) =>
                {
                    if (random.Next() % 2 == 0)
                    {
                        loopState.Break();
                    }
                    if (curItem.Liabilities > 1000)
                    {
                        curItem.Block(cts.Token);
                    }
                });
        }

        private static void UsingParallelForEach(IEnumerable<CreditCard> cards, CancellationTokenSource cts)
        {
            Parallel.ForEach(cards, new ParallelOptions() { CancellationToken = cts.Token }
            , x =>
            {
                if (x.Liabilities > 1000)
                {
                    x.Block(cts.Token);
                }
            });
        }

        private static void UsingAsParallel(IEnumerable<CreditCard> cards, CancellationTokenSource cts)
        {
            cards.AsParallel()
              .WithCancellation(cts.Token)
              .ForAll(x =>
              {
                  if (x.Liabilities > 1000)
                  {
                      x.Block(cts.Token);
                  }
              });
        }
    }

    public class CreditCard
    {
        public decimal Liabilities { get; set; }
        public int Id { get; set; }
        public void Block(CancellationToken ct)
        {
            bool blocked = false;
            for (int i = 0; i < 3; i++)
            {
                ct.ThrowIfCancellationRequested();
                Console.WriteLine($"Connecting {Id}. Iteration: {i}");
                //Connecting to a server
                Thread.Sleep(1000);
                if (i == 3)
                {
                    blocked = true;
                }
            }
            if (blocked)
            {
                Console.WriteLine($"Blocked credit card. ID:{Id}");
            }
        }
    }
}
