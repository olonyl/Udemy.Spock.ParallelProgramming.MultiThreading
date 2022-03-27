using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace _30.ErrorHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            //CompareFlattenVsNormalInnerExceptions();
            TestAggregateException();
            Console.Read();
        }

        private static void TestAggregateException()
        {
            var parent = Task.Factory.StartNew(() =>
            {
                int[] numbers = { 0 };
                var childFactory = new TaskFactory(TaskCreationOptions.AttachedToParent, TaskContinuationOptions.None);
                childFactory.StartNew(() => 5 / numbers[0]);//Division by 0
                childFactory.StartNew(() => numbers[1]);//Index out of range
                childFactory.StartNew(() => { throw null; });//Null reference
            });
            try
            {
                parent.Wait();
            }
            catch (AggregateException ex)
            {

                ex.Flatten().Handle(e =>
                {
                    if (e is DivideByZeroException)
                    {
                        Console.WriteLine("Divide by zero");
                        return true;
                    }
                    if (e is IndexOutOfRangeException)
                    {
                        Console.WriteLine("Index out of range");
                        return true;
                    }
                    Console.WriteLine("Another exception");
                    return true;
                });
            }
        }

        private static void CompareFlattenVsNormalInnerExceptions()
        {
            var t1 = Task.Run(() => Print());
            try
            {
                t1.Wait();
            }
            catch (AggregateException ex)
            {
                var flatternList = ex.Flatten().InnerExceptions;
                foreach (var item in flatternList)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine();

                ReadOnlyCollection<Exception> exs = ex.InnerExceptions;
                foreach (var item in exs)
                {
                    Console.WriteLine(exs);
                }
            }
        }

        public static void Print()
        {
            throw new InvalidOperationException();
        }
    }
}
