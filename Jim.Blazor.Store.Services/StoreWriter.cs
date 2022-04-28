using Jim.Blazor.Store.Models.Options;
using Jim.Core.Extensions;
using Jim.Core.Store.Models.Services;
using Microsoft.JSInterop;
using System.Text.Json;
namespace Jim.Blazor.Store.Services
{
    public class StoreWriter : StoreService, IStoreWriter
    {
        private const string SET_ITEM = "setItem";
        private string SET_ITEM_PATH => _options.StoreToUse + "." + SET_ITEM;

        public StoreWriter(BlazorStoreOptions options, IJSRuntime js) : base(options, js)
        {
        }

        public async Task<bool> WriteAsync<T>(string key, T value) where T : class
        {
            try
            {
                if (key == null)
                    throw new ArgumentNullException(nameof(key));

                string serialised = await ConvertValue(value);

                await _js.InvokeVoidAsync(SET_ITEM_PATH, key, serialised);

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