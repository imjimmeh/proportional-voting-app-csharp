namespace Jim.Core.Multithreading
{
    public interface ISpinLocker
    {
        public SpinLock SpinLock { get; }

        public bool IsLocked { get; }

        public T? LockThenGet<T>(Func<T?> func);

        public Task<T?> LockThenGetAsync<T>(Func<Task<T?>> func);

        public Task LockThenDoAsync<T>(Func<Task> func);

        public void LockThenDo(Action action);
    }
}