using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace _29.CreatingIOBasedTasks
{
    class Program
    {
        private const string FilePath = @"c:\temp\demo.txt";
        static void Main(string[] args)
        {
            //TestTaskWrite();
            //TestEap();
            TestFromAsync();
            Console.Read();
        }

        private static void TestTaskWrite()
        {
            FileStream fs = new FileStream(FilePath
                , FileMode.OpenOrCreate
                , FileAccess.Write
                , FileShare.None
                , 8
                , true);

            string content = "A quick brown fox jumps over the lazy dog - From a Task";
            byte[] data = Encoding.Unicode.GetBytes(content);

            Task task = fs.WriteAsync(data, 0, data.Length);
            task.ContinueWith(t =>
            {
                fs.Close();
                TestAsyncTaskRead();
            });
        }

        private static void TestAsyncTaskRead()
        {
            FileStream fs = new FileStream(FilePath
                , FileMode.OpenOrCreate
                , FileAccess.Read
                , FileShare.None
                , 8
                , true);

            byte[] data = new byte[1024];

            Task<int> readTask = fs.ReadAsync(data, 0, data.Length);
            readTask.ContinueWith(t =>
            {
                fs.Close();
                string content = Encoding.UTF8.GetString(data, 0, t.Result);
                Console.WriteLine($"Read completed. Content is:{content}");
            });
        }

        public static void TestEap()
        {
            WebClient wc = new WebClient();

            Task<byte[]> task = wc.DownloadDataTaskAsync(new Uri("https://google.com"));
            task.ContinueWith(t =>
            {
                Console.WriteLine(Encoding.UTF8.GetString(t.Result));
            });
        }

        public static void TestFromAsync()
        {
            FileStream fs = new FileStream(FilePath
                , FileMode.OpenOrCreate
                , FileAccess.ReadWrite
                , FileShare.None
                , 8
                , true);

            string content = "This is another test";
            byte[] buffer = Encoding.Unicode.GetBytes(content);

            var writeChunk = Task.Factory.FromAsync(fs.BeginWrite, fs.EndWrite, buffer, 0, buffer.Length, null);
            writeChunk.ContinueWith(t =>
            {
                fs.Position = 0;
                var data = new byte[buffer.Length];
                var readChunk = Task<int>.Factory.FromAsync(fs.BeginRead, fs.EndRead, data, 0, data.Length, 0);

                readChunk.ContinueWith(read =>
                {
                    string readResult = Encoding.Unicode.GetString(data, 0, read.Result);
                    Console.WriteLine(readResult);
                });
            });
        }
    }
}
