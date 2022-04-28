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

        public Task<bool> SetItem(string key, string value)
        {
            if (_stores.ContainsKey(key))
                _stores[key] = value;
            else
                _stores.Add(key, value);

            return Task.FromResult(true);
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
