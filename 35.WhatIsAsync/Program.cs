using System;
using System.Net;

namespace _36.WhatIsAsync
{
    class Program
    {
        private static string Url = "https://google.com";
        static void Main(string[] args)
        {
            //DumpPageAsync(Url);
            DumpPageTaskBased(Url);
            Console.WriteLine("\nMain Thread\n");
            Console.Read();
        }

        private static void DumpPage(string uri)
        {
            WebClient wc = new WebClient();
            var result = wc.DownloadString(uri);

            Console.WriteLine(result);
        }

        private async static void DumpPageAsync(string uri)
        {
            WebClient wc = new WebClient();
            var result = await wc.DownloadStringTaskAsync(uri);
            Console.WriteLine(result);
        }
        private static void DumpPageTaskBased(string uri)
        {
            WebClient wc = new WebClient();
            var task = wc.DownloadStringTaskAsync(uri);
            task.ContinueWith(t => Console.WriteLine(t.Result));
        }
    }
}
