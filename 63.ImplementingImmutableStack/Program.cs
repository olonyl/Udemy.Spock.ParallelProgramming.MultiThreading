using System;

namespace _63.ImplementingImmutableStack
{
    class Program
    {
        static void Main(string[] args)
        {
            IStack<int> stack = _63.ImplementingImmutableStack.Stack<int>.Empty;

            IStack<int> stack2 = stack.Push(10);
            IStack<int> stack3 = stack2.Push(20);
            IStack<int> stack4 = stack3.Push(30);

            foreach (var cur in stack4)
            {
                Console.WriteLine(cur);
            }

            Console.Read();
        }

    }
}
