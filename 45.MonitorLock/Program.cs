using System;
using System.Threading;

namespace _45.MonitorLock
{
    class BankCard
    {
        private decimal _moneyAount;
        private readonly object _token = new object();

        public BankCard(decimal moneyAmount)
        {
            _moneyAount = moneyAmount;
        }

        public void ReceivedPayment(decimal amount)
        {
            bool lockTaken = false;
            try
            {

                Monitor.Enter(_token, ref lockTaken);

                _moneyAount += amount;

            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(_token);
                }
            }
        }

        public void TransferToCard(decimal amount, BankCard recipient)
        {
            lock (_token)
            {
                _moneyAount -= amount;

                recipient.ReceivedPayment(amount);
            }
        }

        public void DebitMoney(decimal amount)
        {
            using (_token.Lock(TimeSpan.FromSeconds(3)))
            {
                _moneyAount -= amount;

            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
