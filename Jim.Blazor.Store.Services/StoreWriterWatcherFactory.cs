using Jim.Blazor.Store.Models.Services;
using System.Collections.Concurrent;

namespace Jim.Blazor.Store.Services
{
    public class StoreWriterWatcherFactory : IStoreWriterWatcherFactory
    {
        private SpinLock _spinLock = new SpinLock();
        private bool _lockToken = false;

        private readonly ConcurrentDictionary<Type, IBlazorStoreWriterWatcher> _watchers;
        private readonly IBlazorStoreWriter _storeWriter;

        public StoreWriterWatcherFactory(IBlazorStoreWriter storeWriter)
        {
            _watchers = new ConcurrentDictionary<Type, IBlazorStoreWriterWatcher>();
            _storeWriter = storeWriter ?? throw new ArgumentNullException(nameof(storeWriter));
        }

        public IBlazorStoreWriterWatcher<TValue?> GetWatcher<TValue>()
            where TValue : class?
        {
            if (TryGetExistingWatcher(out IBlazorStoreWriterWatcher<TValue?>? watcher))
            {
                return watcher ?? throw new Exception($"Received success response fetching existing watcher, but reference returned is null");
            }

            watcher = CreateNewWatcher<TValue>();

            return watcher;
        }

        private IBlazorStoreWriterWatcher<TValue?> CreateNewWatcher<TValue>()
            where TValue : class?
        {
            var watcher = new StoreWriterWatcher<TValue?>(_storeWriter);

            DoWithLock(() => _watchers.TryAdd(typeof(TValue), watcher));

            return watcher;
        }

        private void DoWithLock(Action action)
        {
            _lockToken = false;
            try
            {
                _spinLock.Enter(ref _lockToken);
                action();
            }
            catch
            {
                throw;
            }
            finally
            {
                _spinLock.Exit();
            }
        }

        private bool TryGetExistingWatcher<TValue>(out IBlazorStoreWriterWatcher<TValue?>? foundValue)
            where TValue : class?
        {
            if (_watchers.TryGetValue(typeof(TValue), out IBlazorStoreWriterWatcher? existing))
            {
                foundValue = existing as IBlazorStoreWriterWatcher<TValue?> ?? throw new Exception($"Found watcher {existing} but is not type of {typeof(IBlazorStoreWriterWatcher<TValue?>)} - is {existing.GetType()}");

                return true;
            }

            foundValue = null;
            return false;
        }
    }
}
