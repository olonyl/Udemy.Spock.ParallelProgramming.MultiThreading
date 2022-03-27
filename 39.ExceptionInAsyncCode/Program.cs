using System;
using System.Threading.Tasks;

namespace _39.ExceptionInAsyncCode
{
    class Program
    {
        static void Main(string[] args)
        {
            //Catcher();
            CastMultipleExceptionsWithAwait();
            Console.Read();
        }

        private async static void CastMultipleExceptionsWithAwait()
        {
            int[] numbers = { 0 };

            Task<int> t1 = Task.Run(() => 5 / numbers[0]);
            Task<int> t2 = Task.Run(() => numbers[1]);

            Task<int[]> allTasks = Task.WhenAll(t1, t2);
            try
            {
                await allTasks;
            }
            catch (Exception)
            {
                foreach (var ex in allTasks.Exception.InnerExceptions)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private static async Task Catcher()
        {
            try
            {
                var thrower = Thrower();
                await thrower;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        async static Task Thrower()
        {
            await Task.Delay(1000);
            throw new InvalidOperationException();
        }
    }
}
