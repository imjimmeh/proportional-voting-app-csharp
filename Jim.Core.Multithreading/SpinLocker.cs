namespace Jim.Core.Multithreading
{
    public class SpinLocker : ISpinLocker
    {
        private bool _lockToken;
        private SpinLock _lock;

        public SpinLocker()
        {
            _lock = new SpinLock();
            _lockToken = false;
        }

        public SpinLock SpinLock => _lock;
        public bool IsLocked => _lockToken;

        public void LockThenDo(Action action)
        {
            _lockToken = false;
            try
            {
                _lock.Enter(ref _lockToken);
                action();
            }
            finally
            {
                _lock.Exit();
            }
        }


        public async Task LockThenDoAsync<T>(Func<Task> func)
        {
            _lockToken = false;
            try
            {
                _lock.Enter(ref _lockToken);
                await func();
            }
            finally
            {
                _lock.Exit();
            }
        }

        public Task<T?> LockThenGetAsync<T>(Func<Task<T?>> func)
        {
            _lockToken = false;
            try
            {
                _lock.Enter(ref _lockToken);
                var result = func();

                return result;
            }
            finally
            {
                _lock.Exit();
            }
        }

        public T? LockThenGet<T>(Func<T?> func)
        {
            _lockToken = false;
            try
            {
                _lock.Enter(ref _lockToken);
                var result = func();

                return result;
            }
            finally
            {
                _lock.Exit();
            }
        }
    }
}
