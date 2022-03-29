using System;
using System.Threading;

namespace _46.ReaderWriterLockSlimExample
{
    class BankCard
    {
        private decimal _moneyAount;
        private decimal _credit;
        private readonly object _token = new object();
        private ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();

        public BankCard(decimal moneyAmount)
        {
            _moneyAount = moneyAmount;
        }

        public decimal TotalMoneyAmount
        {
            get
            {
                using (_rwLock.TakeReaderLock(TimeSpan.FromSeconds(3)))
                {
                    return _moneyAount + _credit;
                }
            }
        }
        public void ReceivedPayment(decimal amount)
        {
            using (_rwLock.TakeWriteLock(TimeSpan.FromSeconds(3)))
            {
                _moneyAount += amount;
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

    }
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
