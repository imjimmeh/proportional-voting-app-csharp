using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Jim.Blazor.Store.Tests.Unit.Mocks
{
    public class StoreMocker : IStoreMocker
    {
        private bool _lockToken;
        private ConcurrentDictionary<string, string?> _stores;
        private SpinLock _lock = new SpinLock();

        public StoreMocker()
        {
            _stores = new ConcurrentDictionary<string, string?>();
        }

        public Task<bool> SetItem(string key, string? value)
        {
            AddOrUpdate(key, value);
            return Task.FromResult(true);
        }

        private void LockThenDo(Action action)
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

        private void LockThenGet<T>(Func<T> func)
        {
            _lockToken = false;
            try
            {
                _lock.Enter(ref _lockToken);
                func();
            }
            finally
            {
                _lock.Exit();
            }
        }

        private void AddOrUpdate(string key, string? value)
        {
            LockThenDo(() =>
            {
                if (_stores.TryGetValue(key, out string? foundValue))
                {
                    _stores.TryUpdate(key, value, foundValue);
                }
                else
                {
                    _stores.TryAdd(key, value);
                }
            });
        }

        public async Task<string?> GetItem(string key)
        {
            if (_stores.TryGetValue(key, out string? value))
                return value;
            else
                return null;
        }
    }
}
