using Jim.Blazor.Store.Models;
using Jim.Core.Extensions;
using Jim.Core.Store.Models.Services;
using Microsoft.JSInterop;

namespace Jim.Blazor.Store.Services
{
    public class StoreReader : StoreService, IStoreReader
    {
        private const string GET_ITEM = "getItem";
        private string GET_ITEM_PATH => _options.StoreToUse + "." + GET_ITEM;

        public StoreReader(BlazorStoreOptions options, IJSRuntime js) : base(options, js)
        {
        }

        public async Task<T?> GetAsync<T>(string key)
            where T : class
        {
            try
            {
                string value = await GetFromStore(key);

                if (value.TryDeserialise(out T? deserialised))
                    return deserialised ?? throw new Exception("Deserialisation was reported success, but value returned is null");

                return null;
            }
            catch (KeyNotFoundException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<string> GetFromStore(string key)
        {
            try
            {
                string result = await _js.InvokeAsync<string>(GET_ITEM_PATH, key);

                if (string.IsNullOrEmpty(result))
                    throw new KeyNotFoundException($"Could not find key in store, or value found was null or empty");

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting {key} from {_options.StoreToUse}", ex);
            }
        }

    }
}