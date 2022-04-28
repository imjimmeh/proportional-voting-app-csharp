using Jim.Blazor.Store.Models.Events;
using Jim.Blazor.Store.Models.Options;
using Jim.Blazor.Store.Models.Services;
using Jim.Blazor.Store.Services.Stores;
using Microsoft.JSInterop;

namespace Jim.Blazor.Store.Services
{
    public class StoreWriterWatcher : StoreWriter, IBlazorStoreWriterWatcher
    {
        public StoreWriterWatcher(BlazorStoreOptions options, IJSRuntime js) : base(options, js)
        {
        }

        public event EventHandler<BlazorStoreEntryChangedEventArgs>? OnStoreWrite;

        public override async Task<bool> WriteAsync<T>(string key, T value) where T : class
        {
            try
            {
                var result = await base.WriteAsync(key, value);

                if (result)
                    OnStoreWrite?.Invoke(this, GenerateEventArgs(key));

                return result;
            }
            catch
            {
                throw;
            }
        }

        private BlazorStoreEntryChangedEventArgs GenerateEventArgs(string key)
            => new BlazorStoreEntryChangedEventArgs
            {
                Key = key,
                Method = JsStoreMethod.Get,
                StoreType = Options.StoreToUse.ToStoreType()
            };
    }
}
