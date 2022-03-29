using System;
using System.Threading;
using System.Threading.Tasks;

namespace _53.CountDownEventAndBarrier
{
    class Program
    {
        private static CountdownEvent _countdown = new CountdownEvent(4);
        private static readonly Barrier Barrier = new Barrier(participantCount: 0);
        static void Main(string[] args)
        {
            //TestCountdown();
            var records = GetNumberOfRecords();

            Task[] tasks = new Task[records];

            for (int i = 0; i < records; i++)
            {
                Barrier.AddParticipant();
                int j = i;
                tasks[j] = Task.Factory.StartNew(() =>
                {
                    GetDataAndStoreData(j);
                });
            }

            Task.WaitAll(tasks);
            Console.WriteLine("Backup was completed");

            Console.Read();
        }

        private static int GetNumberOfRecords()
        {
            return 10;
        }

        private static void GetDataAndStoreData(int j)
        {
            Console.WriteLine($"Getting data from server: {j}");
            Thread.Sleep(TimeSpan.FromSeconds(2));

            Barrier.SignalAndWait();

            Console.WriteLine($"Send data to Backup server: {j}");

            Barrier.SignalAndWait();


        }

        private static void TestCountdown()
        {
            Task.Run(() => DoWork());
            Task.Run(() => DoWork());
            Task.Run(() => DoWork());

            _countdown.Wait();

            Console.WriteLine("All tasks have finished their work");
        }

        private static void DoWork()
        {
            Thread.Sleep(1000);
            Console.WriteLine($"I am a task with id: {Task.CurrentId}");
            _countdown.Signal();
        }
    }

}
