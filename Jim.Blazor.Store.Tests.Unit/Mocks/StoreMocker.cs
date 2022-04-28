using Jim.Core.Multithreading;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Jim.Blazor.Store.Tests.Unit.Mocks
{
    public class StoreMocker : IStoreMocker
    {
        private ConcurrentDictionary<string, string?> _stores;

        private ISpinLocker _locker;

        public StoreMocker()
        {
            _stores = new ConcurrentDictionary<string, string?>();
            _locker = new SpinLocker();
        }

        public Task<bool> SetItem(string key, string? value)
        {
            AddOrUpdate(key, value);
            return Task.FromResult(true);
        }

        
        private void AddOrUpdate(string key, string? value)
        {
            _locker.LockThenDo(() =>
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
