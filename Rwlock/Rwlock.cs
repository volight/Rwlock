using System;
using System.Threading;

namespace Volight.LockWraps
{
    public sealed class Rwlock : IDisposable
    {
        readonly ReaderWriterLockSlim locker = new(LockRecursionPolicy.NoRecursion);

        #region Dispose

        volatile bool disposed = false;
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            if (disposed) return;
            disposed = true;
            locker.Dispose();
        }
        ~Rwlock() => Dispose();

        #endregion

        public ReadGuard Read() => new ReadGuard(this).Enter();
        public ReadGuard? TryRead(int millisecondsTimeout) => new ReadGuard(this).TryEnter(millisecondsTimeout);
        public ReadGuard? TryRead(TimeSpan timeout) => new ReadGuard(this).TryEnter(timeout);
        public WriteGuard Write() => new WriteGuard(this).Enter();
        public WriteGuard? TryWrite(int millisecondsTimeout) => new WriteGuard(this).TryEnter(millisecondsTimeout);
        public WriteGuard? TryWrite(TimeSpan timeout) => new WriteGuard(this).TryEnter(timeout);

        public sealed class ReadGuard : IDisposable
        {
            readonly Rwlock locker;

            internal ReadGuard(Rwlock locker)
            {
                this.locker = locker;
            }

            internal ReadGuard Enter()
            {
                locker.locker.EnterReadLock();
                return this;
            }

            internal ReadGuard? TryEnter(int millisecondsTimeout)
            {
                if (locker.locker.TryEnterReadLock(millisecondsTimeout)) return this;
                else return null;
            }
            internal ReadGuard? TryEnter(TimeSpan timeout)
            {
                if (locker.locker.TryEnterReadLock(timeout)) return this;
                else return null;
            }

            #region Dispose

            volatile bool disposed = false;
            public void Dispose()
            {
                GC.SuppressFinalize(this);
                if (disposed) return;
                disposed = true;
                locker.locker.ExitReadLock();
            }
            ~ReadGuard() => Dispose();

            #endregion
        }

        public sealed class WriteGuard : IDisposable
        {
            readonly Rwlock locker;

            internal WriteGuard(Rwlock locker)
            {
                this.locker = locker;
            }

            internal WriteGuard Enter()
            {
                locker.locker.EnterWriteLock();
                return this;
            }

            internal WriteGuard? TryEnter(int millisecondsTimeout)
            {
                if (locker.locker.TryEnterWriteLock(millisecondsTimeout)) return this;
                else return null;
            }

            internal WriteGuard? TryEnter(TimeSpan timeout)
            {
                if (locker.locker.TryEnterWriteLock(timeout)) return this;
                else return null;
            }

            #region Dispose

            volatile bool disposed = false;
            public void Dispose()
            {
                GC.SuppressFinalize(this);
                if (disposed) return;
                disposed = true;
                locker.locker.ExitWriteLock();
            }
            ~WriteGuard() => Dispose();

            #endregion
        }
    }

    public sealed class Rwlock<T> : IDisposable
    {
        public Rwlock(T value) => this.value = value;

        T value;
        readonly ReaderWriterLockSlim locker = new(LockRecursionPolicy.NoRecursion);

        #region Dispose

        volatile bool disposed = false;
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            if (disposed) return;
            disposed = true;
            locker.Dispose();
        }
        ~Rwlock() => Dispose();

        #endregion

        public ReadGuard Read() => new ReadGuard(this).Enter();
        public ReadGuard? TryRead(int millisecondsTimeout) => new ReadGuard(this).TryEnter(millisecondsTimeout);
        public ReadGuard? TryRead(TimeSpan timeout) => new ReadGuard(this).TryEnter(timeout);
        public WriteGuard Write() => new WriteGuard(this).Enter();
        public WriteGuard? TryWrite(int millisecondsTimeout) => new WriteGuard(this).TryEnter(millisecondsTimeout);
        public WriteGuard? TryWrite(TimeSpan timeout) => new WriteGuard(this).TryEnter(timeout);

        public sealed class ReadGuard : IDisposable
        {
            readonly Rwlock<T> locker;

            public T Value { get => locker.value; set => locker.value = value; }

            internal ReadGuard(Rwlock<T> locker)
            {
                this.locker = locker;
            }

            internal ReadGuard Enter()
            {
                locker.locker.EnterReadLock();
                return this;
            }

            internal ReadGuard? TryEnter(int millisecondsTimeout)
            {
                if (locker.locker.TryEnterReadLock(millisecondsTimeout)) return this;
                else return null;
            }
            internal ReadGuard? TryEnter(TimeSpan timeout)
            {
                if (locker.locker.TryEnterReadLock(timeout)) return this;
                else return null;
            }

            #region Dispose

            volatile bool disposed = false;
            public void Dispose()
            {
                GC.SuppressFinalize(this);
                if (disposed) return;
                disposed = true;
                locker.locker.ExitReadLock();
            }
            ~ReadGuard() => Dispose();

            #endregion
        }

        public sealed class WriteGuard : IDisposable
        {
            readonly Rwlock<T> locker;

            public T Value { get => locker.value; set => locker.value = value; }

            internal WriteGuard(Rwlock<T> locker)
            {
                this.locker = locker;
            }

            internal WriteGuard Enter()
            {
                locker.locker.EnterWriteLock();
                return this;
            }

            internal WriteGuard? TryEnter(int millisecondsTimeout)
            {
                if (locker.locker.TryEnterWriteLock(millisecondsTimeout)) return this;
                else return null;
            }

            internal WriteGuard? TryEnter(TimeSpan timeout)
            {
                if (locker.locker.TryEnterWriteLock(timeout)) return this;
                else return null;
            }

            #region Dispose

            volatile bool disposed = false;
            public void Dispose()
            {
                GC.SuppressFinalize(this);
                if (disposed) return;
                disposed = true;
                locker.locker.ExitWriteLock();
            }
            ~WriteGuard() => Dispose();

            #endregion
        }
    }

}
