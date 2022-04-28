using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jim.Blazor.Store.Tests.Unit.Mocks
{
    public class StoreMocker : IStoreMocker
    {
        private Dictionary<string, string> _stores;

        public StoreMocker(int capacity = 20)
        {
            _stores = new Dictionary<string, string>(capacity);
        }

        public Task<bool> SetItem(string key, string? value)
        {
            bool keyExists = _stores.ContainsKey(key);

            if (keyExists)
            {
                AddOrUpdate(key, value);
            }
            else
            {
                AddIfNotNull(key, value);
            }
            return Task.FromResult(true);
        }

        private void AddIfNotNull(string key, string? value)
        {
            if (!string.IsNullOrEmpty(value))
                _stores.Add(key, value);
        }

        private void AddOrUpdate(string key, string? value)
        {
            if (string.IsNullOrEmpty(value))
                _stores.Remove(key);
            else
                _stores[key] = value;
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
