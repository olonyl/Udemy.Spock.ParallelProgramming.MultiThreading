using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace _52.AutoResetEventManualResetEventSlim
{
    class BankTerminal
    {
        private readonly Protocol _protocol;
        //private readonly ManualResetEventSlim _operationSignal = new ManualResetEventSlim(false);

        private readonly AutoResetEvent _operationSignal = new AutoResetEvent(false);

        public BankTerminal(IPEndPoint endPoint)
        {
            _protocol = new Protocol(endPoint);
            _protocol.OnMessageReceived += OnMessageReceived;
        }

        private void OnMessageReceived(object sender, ProtocolMessage e)
        {
            if (e.Status == OperationStatus.Finished)
            {
                Console.WriteLine("Signaling!");
                _operationSignal.Set();
            }
        }

        public Task Purchase(decimal amount)
        {
            return Task.Run(() =>
            {
                const int purchaseOpCode = 1;
                _protocol.Send(purchaseOpCode, amount);

                //_operationSignal.Reset();
                Console.WriteLine("Waiting for signal");
                // _operationSignal.Wait();
                _operationSignal.WaitOne();
            });
        }
    }

    class Protocol
    {
        private readonly IPEndPoint _endPoint;

        public Protocol(IPEndPoint endPoint)
        {
            _endPoint = endPoint;
        }

        public event EventHandler<ProtocolMessage> OnMessageReceived;

        public void Send(int opCode, object parameters)
        {
            Task.Run(() =>
            {
                Console.WriteLine("Operation is in action");
                Thread.Sleep(3000);
                OnMessageReceived?.Invoke(this, new ProtocolMessage(OperationStatus.Finished));
            });
        }
    }

    public enum OperationStatus
    {
        Finished,
        Launched
    }

    public class ProtocolMessage
    {
        public OperationStatus Status { get; }

        public ProtocolMessage(OperationStatus status)
        {
            Status = status;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            var bt = new BankTerminal(new IPEndPoint(new IPAddress(0x2413188f), 8080));

            Task purchaseTask = bt.Purchase(100);
            var firstContinue = purchaseTask.ContinueWith(x => Console.WriteLine("First Operation is done"));

            firstContinue.ContinueWith(x =>
            {
                Console.WriteLine("--------------- Another Operation ---------------");
                Task secondPurchase = bt.Purchase(200);
                secondPurchase.ContinueWith(y => Console.WriteLine("Second Operation is Done"));
            });

            Console.ReadLine();

        }
    }
}
