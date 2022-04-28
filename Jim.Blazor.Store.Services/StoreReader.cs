using Jim.Blazor.Store.Models.Options;
using Jim.Core.Extensions;
using Jim.Core.Store.Models.Services;
using Microsoft.JSInterop;

namespace Jim.Blazor.Store.Services
{
    public class StoreReader : StoreService, IStoreReader
    {
        private readonly string _getItemMethod = JsStoreMethod.Get.ToMethodName();
        private string _getItemPath;

        public StoreReader(BlazorStoreOptions options, IJSRuntime js) : base(options, js)
        {
        }

        public string GetItemPath
        { 
            get 
            {
                if (_getItemPath == null)
                    _getItemPath = _options.GetMethodPath(_getItemMethod);

                return _getItemPath;
            }
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
                string result = await _js.InvokeAsync<string>(GetItemPath, key);

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