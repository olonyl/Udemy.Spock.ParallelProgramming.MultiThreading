using System.Collections.Generic;

namespace _63.ImplementingImmutableStack
{
    public interface IStack<T> : IEnumerable<T>
    {
        IStack<T> Push(T value);
        IStack<T> Pop();
        T Peek();
        bool IsEmpty { get; }
    }
}
