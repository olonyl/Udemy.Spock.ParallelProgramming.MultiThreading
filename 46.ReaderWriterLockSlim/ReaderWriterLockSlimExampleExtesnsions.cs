using System;
using System.Threading;

namespace _46.ReaderWriterLockSlimExample
{
    public static class ReaderWriterLockSlimExampleExtesnsions
    {
        public static ReaderLockSlimWrapper TakeReaderLock(this ReaderWriterLockSlim rwlock, TimeSpan timeout)
        {
            bool taken = false;
            try
            {
                taken = rwlock.TryEnterReadLock(timeout);
                if (taken)
                {
                    return new ReaderLockSlimWrapper(rwlock);
                }
                throw new TimeoutException("Failed to acquire a ReadWriterLockSlim object in read mode");
            }
            catch (Exception)
            {
                if (taken)
                {
                    rwlock.ExitReadLock();
                }
                throw;
            }

        }

        public static WriterLockSlimWrapper TakeWriteLock(this ReaderWriterLockSlim rwlock, TimeSpan timeout)
        {
            bool taken = false;
            try
            {
                taken = rwlock.TryEnterWriteLock(timeout);
                if (taken)
                {
                    return new WriterLockSlimWrapper(rwlock);
                }
                throw new TimeoutException("Failed to acquire a ReadWriterLockSlim object in write mode");
            }
            catch (Exception)
            {
                if (taken)
                {
                    rwlock.ExitWriteLock();
                }
                throw;
            }

        }
    }

    public struct ReaderLockSlimWrapper : IDisposable
    {
        private readonly ReaderWriterLockSlim _rwlock;

        public ReaderLockSlimWrapper(ReaderWriterLockSlim rwlock)
        {
            _rwlock = rwlock;
        }

        public void Dispose()
        {
            _rwlock.ExitReadLock();
        }
    }

    public struct WriterLockSlimWrapper : IDisposable
    {
        private readonly ReaderWriterLockSlim _rwlock;

        public WriterLockSlimWrapper(ReaderWriterLockSlim rwlock)
        {
            _rwlock = rwlock;
        }

        public void Dispose()
        {
            _rwlock.ExitWriteLock();
        }
    }
}