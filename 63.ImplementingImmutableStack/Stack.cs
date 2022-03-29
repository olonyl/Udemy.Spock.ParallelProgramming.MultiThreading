using System;
using System.Collections;
using System.Collections.Generic;

namespace _63.ImplementingImmutableStack
{
    public sealed class Stack<T> : IStack<T>
    {
        private static readonly EmptyStack _empty = new EmptyStack();
        public bool IsEmpty => false;
        public static IStack<T> Empty => _empty;
        private readonly T _head;
        private readonly IStack<T> _tail;
        public Stack(T head, IStack<T> tail)
        {
            _head = head;
            _tail = tail;
        }
        public IEnumerator<T> GetEnumerator()
        {
            for (IStack<T> stack = this; !stack.IsEmpty; stack = stack.Pop())
            {
                yield return stack.Peek();
            }
        }

        public T Peek()
        {
            return _head;
        }

        public IStack<T> Pop()
        {
            return _tail;
        }

        public IStack<T> Push(T value)
        {
            return new Stack<T>(value, this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private sealed class EmptyStack : IStack<T>
        {
            public bool IsEmpty => true;

            public IEnumerator<T> GetEnumerator()
            {
                yield break;
            }

            public T Peek()
            {
                throw new Exception("Empty stack");
            }

            public IStack<T> Pop()
            {
                throw new Exception("Empty stack");
            }

            public IStack<T> Push(T value)
            {
                return new Stack<T>(value, this);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }
    }
}
