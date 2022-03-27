using System;
using System.Threading.Tasks;

namespace _39.ExceptionInAsyncCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Catcher();
            Console.Read();
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
