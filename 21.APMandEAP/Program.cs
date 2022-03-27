using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace _21.APMandEAP
{
    class Program
    {
        private const string FilePath = @"c:\temp\demo.txt";
        static void Main(string[] args)
        {
            //This Method implements APM, Asynchronous Programming Model
            TestWrite();

            //This Method implements EAP, Event-Based Asynchronous Pattern
            TestEAP();
            Thread.Sleep(60000);
        }

        private static void TestWrite()
        {
            FileStream fs = new FileStream(FilePath
                , FileMode.OpenOrCreate
                , FileAccess.Write
                , FileShare.None
                , 8
                , FileOptions.Asynchronous);

            string content = "A quick brown fox jumps over the lazy dog";
            byte[] data = Encoding.Unicode.GetBytes(content);

            Console.WriteLine("Begin to write");
            fs.BeginWrite(data, 0, data.Length, OnWriteCompleted, fs);
            Console.WriteLine("Write queued");
        }

        private static void OnWriteCompleted(IAsyncResult asyncResult)
        {
            FileStream fs = (FileStream)asyncResult.AsyncState;
            fs.EndWrite(asyncResult);

            fs.Close();
            Console.WriteLine("Write completed");

            TestRead();
        }

        private static void TestRead()
        {
            FileStream fs = new FileStream(FilePath
                , FileMode.OpenOrCreate
                , FileAccess.Read
                , FileShare.None
                , 8
                , FileOptions.Asynchronous);
            byte[] data = new byte[1024];

            Console.WriteLine("Begin to read");
            fs.BeginRead(data, 0, data.Length, OnReadCompleted, new { Stream = fs, Data = data });
            Console.WriteLine("Read queued");
        }

        private static void OnReadCompleted(IAsyncResult asyncResult)
        {
            dynamic state = asyncResult.AsyncState;

            int bytesRead = state.Stream.EndRead(asyncResult);

            byte[] data = state.Data;
            string content = Encoding.Unicode.GetString(data, 0, bytesRead);

            Console.WriteLine("read completed. Content is: {0}", content);
            state.Stream.Close();

            Console.ReadKey();
        }

        private static void TestEAP()
        {
            WebClient wc = new WebClient();

            wc.DownloadDataCompleted += (s, e) => Console.WriteLine(Encoding.UTF8.GetString(e.Result));
            wc.DownloadDataAsync(new Uri("http://www.google.com"));

            Console.ReadKey();
        }
    }
}
