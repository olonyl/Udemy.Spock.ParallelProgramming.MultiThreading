using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace _64ImmutableStackAndQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            StackDemo();
            QueueDemo();
            Console.Read();
        }

        private static void QueueDemo()
        {
            var queue = ImmutableQueue<int>.Empty;
            queue = queue.Enqueue(1).Enqueue(2);

            PrintOutCollection(queue);

            int first = queue.Peek();
            Console.WriteLine($"Last item:{first}");

            queue = queue.Dequeue(out first);
            Console.WriteLine($"Last item:{first}; Last After Pop:{queue.Peek()}");

        }

        private static void PrintOutCollection(ImmutableQueue<int> queue)
        {

        }

        static void StackDemo()
        {
            var stack = ImmutableStack<int>.Empty;
            stack = stack.Push(1).Push(2);

            int last = stack.Peek();
            Console.WriteLine($"Last item:{last}");

            stack = stack.Pop(out last);

            Console.WriteLine($"Last item:{last}; Last after Pop: {stack.Peek()}");
        }

        private static void PirntOutCollection<T>(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                Console.WriteLine(item);
            }
        }
    }


}
