using System.Collections.Generic;
using System.Collections.Immutable;

namespace _68.BuildersInImmutableCollections
{

    class Program
    {
        private static readonly List<int> largeList = new List<int>(128);

        private static void GenerateList()
        {
            for (int i = 0; i < 100000; i++)
            {
                largeList.Add(i);
            }
        }

        private static void BuildImmutableCollectionDemo()
        {
            //Method  #1
            var builder = ImmutableList.CreateBuilder<int>();
            foreach (var item in largeList)
            {
                builder.Add(item);
            }
            ImmutableList<int> immutableList = builder.ToImmutable();

            //Method #2
            var list = largeList.ToImmutableList();


        }

        static void Main(string[] args)
        {
        }
    }
}
