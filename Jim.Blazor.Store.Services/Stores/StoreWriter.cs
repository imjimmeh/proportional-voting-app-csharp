using Jim.Blazor.Store.Models.Options;
using Jim.Blazor.Store.Models.Services;
using Jim.Core.Extensions;
using Microsoft.JSInterop;

namespace Jim.Blazor.Store.Services.Stores
{
    public class StoreWriter : StoreService, IBlazorStoreWriter
    {
        public StoreWriter(BlazorStoreOptions options, IJSRuntime js) : base(JsStoreMethod.Set, options, js)
        {
        }

        public virtual async Task<bool> WriteAsync<T>(string key, T value) where T : class
        {
            try
            {
                if (key == null)
                    throw new ArgumentNullException(nameof(key));

                string serialised = await ConvertValue(value);

                await _js.InvokeVoidAsync(methodPath, key, serialised);

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<string> ConvertValue<T>(T value) where T : class
        {
            string serialised = value == null ? "" : value.Serialise();

            return serialised;
        }
    }
}